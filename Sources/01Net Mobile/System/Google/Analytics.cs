//*************************************************************************************************
// DEBUT DU FICHIER
//*************************************************************************************************

//*************************************************************************************************
// Nom           : Analytics.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation des objets du namespace System.Google
// Environnement : Visual Studio 2010
// Créé le       : 17/11/2013
// Modifié le    : 23/03/2015
//*************************************************************************************************

//-------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Phone.Infos;
using System.Net.NetworkInformation;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Microsoft.Phone.Info;
//-------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------

//*************************************************************************************************
// Début du bloc "System.Google"
//*************************************************************************************************
namespace System.Google
	{

    //   ###   #   #   ###   #      #   #  #####  #   ###    ####
    //  #   #  ##  #  #   #  #       # #     #    #  #   #  #    
    //  #####  # # #  #####  #        #      #    #  #       ### 
    //  #   #  #  ##  #   #  #        #      #    #  #   #      #
    //  #   #  #   #  #   #  #####    #      #    #   ###   #### 

	//*********************************************************************************************
	// Classe Analytics
	//*********************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit une méthode de suivi de l'activité d'une application Windows-Phone.
	/// </summary>
	//---------------------------------------------------------------------------------------------
	public sealed class Analytics
		{
		//-----------------------------------------------------------------------------------------
		// Section des Attributs Statiques
		//-----------------------------------------------------------------------------------------
        private static readonly Uri EndPointSecure   = 
		                                    new Uri ( "https://ssl.google-analytics.com/collect" );
        private static readonly Uri EndPointUnsecure = 
		                                    new Uri ( "http://www.google-analytics.com/collect"  );
		//-----------------------------------------------------------------------------------------

		//-----------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------
		private string  PropertyId = "";
		private bool    UseSecure  = false;
		private string  AppName    = "";
		private Version AppVersion = new Version ( 1, 0, 0, 0 );
		private string  ClientID   = "";
		//-----------------------------------------------------------------------------------------
		
		//*****************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------

		//*****************************************************************************************
		// Prototype   : public Analytics ( string PropertyId, string AppName, string ClientID )
		// Description : Constructeur de la classe
		//*****************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>Analytics</b>.
		/// </summary>
		/// <param name="PropertyId">ID de suivi.</param>
		/// <param name="AppName">Nom de l'app.</param>
		/// <param name="ClientID">Identifiant du client.</param>
		//-----------------------------------------------------------------------------------------
		public Analytics ( string PropertyId, string AppName, string ClientID ) : 
			this ( PropertyId, false, AppName, new Version ( 1, 0, 0, 0 ), ClientID ) {}
		//*****************************************************************************************

		//*****************************************************************************************
		// Prototype   : public Analytics ( string PropertyId, string AppName, 
		//                                                   Version AppVersion, string ClientID )
		// Description : Constructeur de la classe
		//*****************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>Analytics</b>.
		/// </summary>
		/// <param name="PropertyId">ID de suivi.</param>
		/// <param name="AppName">Nom de l'app.</param>
		/// <param name="AppVersion">Version de l'app.</param>
		/// <param name="ClientID">Identifiant du client.</param>
		//-----------------------------------------------------------------------------------------
		public Analytics ( string PropertyId, string AppName, Version AppVersion, 
		                                                                        string ClientID ) : 
			this ( PropertyId, false, AppName, AppVersion, ClientID ) {}
		//*****************************************************************************************

		//*****************************************************************************************
		// Prototype   : public Analytics ( string PropertyId, bool UseSecure, 
		//                                                     string AppName, Version AppVersion )
		// Description : Constructeur de la classe
		//*****************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>Analytics</b>.
		/// </summary>
		/// <param name="PropertyId">ID de suivi.</param>
		/// <param name="UseSecure">Indique s'il faut utiliser le https.</param>
		/// <param name="AppName">Nom de l'app.</param>
		/// <param name="AppVersion">Version de l'app.</param>
		/// <param name="ClientID">Identifiant du client.</param>
		//-----------------------------------------------------------------------------------------
		public Analytics ( string PropertyId, bool UseSecure, string AppName, 
		                                                      Version AppVersion, string ClientID )
			{
			//-------------------------------------------------------------------------------------
			this.PropertyId       = PropertyId;
			this.UseSecure        = UseSecure;
			this.AppName          = AppName;
			this.AppVersion       = AppVersion;
			this.ClientID         = ClientID;
			this.CustomDimentions = new Dictionary<int,string> ();
			//-------------------------------------------------------------------------------------
			}
		//*****************************************************************************************
		
		//-----------------------------------------------------------------------------------------
		#endregion
		//*****************************************************************************************
		
		//*****************************************************************************************
		#region // Section des Procédures Privées
		//-----------------------------------------------------------------------------------------

		//*****************************************************************************************
		// Prototype   : private static string GetUrlEncodedString 
		//                                                     ( Dictionary<string, string> Datas )
		// Description : Traduit le tableau en Url
		//*****************************************************************************************
		/// <summary>
		/// Traduit le tableau en Url.
		/// </summary>
		/// <param name="NameValueCollection">Tableau à traduire.</param>
		/// <returns>Url correspondante.</returns>
		//-----------------------------------------------------------------------------------------
		private static string GetUrlEncodedString ( Dictionary<string, string> Datas )
			{
			//-------------------------------------------------------------------------------------
			bool isFirst = true;
			var  result  = new StringBuilder ();
			//-------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------
			foreach ( var Item in Datas )
				{
				//---------------------------------------------------------------------------------
				if ( isFirst ) isFirst = false;
				else           result.Append ( "&" );
				//---------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------
				result.Append ( Item.Key );
				result.Append ( "="      );

				if ( Item.Value == null ) result.Append ( "" );
				else                      result.Append ( HttpUtility.UrlEncode ( Item.Value ) );
				//---------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------
			return result.ToString ();
			//-------------------------------------------------------------------------------------
			}
		//*****************************************************************************************
			
		//-----------------------------------------------------------------------------------------
		#endregion
		//*****************************************************************************************
		
		//*****************************************************************************************
		#region // Section des Procédures Publiques
		//----------------------------------------------------------------------------------------
		
		//*****************************************************************************************
		// Prototype   : public void SendView ( string ViewName, params ViewArgument[] Args )
		// Description : Informe le service qu'une vue a été chargée
		//*****************************************************************************************
		/// <summary>
		/// Informe le service qu'une vue a été chargée.
		/// </summary>
		/// <param name="ViewName">Nom de la vue.</param>
		/// <param name="Args">Arguments de la vue.</param>
		//----------------------------------------------------------------------------------------
		public void SendView ( string ViewName, params object[] Args )
			{
			//-------------------------------------------------------------------------------------
			if ( ! NetworkInterface.GetIsNetworkAvailable () ) return;

			string Line = "";
			//-------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------
			foreach ( object Arg in Args )
				{
				//---------------------------------------------------------------------------------
				if ( Arg != null )
					{
					if ( string.IsNullOrEmpty ( Line ) )
						Line += ( " : " + string.Format ( "{0}", Arg ) );
					else
						Line += ( ", " + string.Format ( "{0}", Arg ) );
					}
				//---------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------
			
			//-------------------------------------------------------------------------------------
			this.SendViewInternal ( ViewName + Line );
			//-------------------------------------------------------------------------------------
			}
		//*****************************************************************************************

		//*****************************************************************************************
		// Prototype   : public void SendViewInternal ( string ViewName )
		// Description : Informe le service qu'une vue a été chargée
		//*****************************************************************************************
		/// <summary>
		/// Informe le service qu'une vue a été chargée.
		/// </summary>
		/// <param name="ViewName">Nom de la vue.</param>
		//-----------------------------------------------------------------------------------------
		private void SendViewInternal ( string ViewName )
			{
			//-------------------------------------------------------------------------------------
			Size Sr = ScreenResolution;
			
			var PayloadData = new Dictionary<string, string>();

			PayloadData.Add ( "v"  , "1"                                              );
			PayloadData.Add ( "tid", this.PropertyId                                  );
			PayloadData.Add ( "cid", this.ClientID                                    );
			PayloadData.Add ( "an" , this.AppName                                     );
			PayloadData.Add ( "av" , this.AppVersion.ToString ()                      );
			PayloadData.Add ( "t"  , "appview"                                        );
			PayloadData.Add ( "cd" , ViewName                                         );
			PayloadData.Add ( "sr" , string.Format ( "{0}x{1}", Sr.Width, Sr.Height ) );
			PayloadData.Add ( "vp" , string.Format ( "{0}x{1}", Sr.Width, Sr.Height ) );
			PayloadData.Add ( "ul" , UserLanguage.ToLowerInvariant ()                 );
			//-------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------
			foreach ( KeyValuePair<int, string> Cd in this.CustomDimentions )
				{ PayloadData.Add ( string.Format ( "cd{0}", Cd.Key ), Cd.Value ); }
			//-------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------
			SendOrPostCallback Event = ( object Args )=>
				{
				//---------------------------------------------------------------------------------
				Dictionary<string, string> Data = (Dictionary<string, string>)Args;

				var    EndPoint = this.UseSecure ? EndPointSecure : EndPointUnsecure;
				byte[] Content  = Encoding.UTF8.GetBytes ( GetUrlEncodedString ( Data ) );
				//---------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------
				try
					{
					//-----------------------------------------------------------------------------
					HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create ( EndPoint );

					http.Method      = "POST";
					http.ContentType = "application/x-www-form-urlencoded";
					http.UserAgent   = DeviceInfos.UserAgent;

					http.BeginGetRequestStream ( ( IAsyncResult A ) =>
						{
						//-------------------------------------------------------------------------
						try
							{
							//---------------------------------------------------------------------
							HttpWebRequest Request_A = (HttpWebRequest)A.AsyncState;

							Stream Stream = Request_A.EndGetRequestStream ( A );
						
							Stream.Write ( Content, 0, Content.Length );

							Stream.Close ();
							//---------------------------------------------------------------------

							//---------------------------------------------------------------------
							Request_A.BeginGetResponse ( ( IAsyncResult B ) =>
								{
								//-----------------------------------------------------------------
								try
									{
									//-------------------------------------------------------------
									HttpWebRequest Request_B = (HttpWebRequest)B.AsyncState;

									WebResponse Response = Request_B.EndGetResponse ( B );

									HttpWebResponse HttpResponse = Response as HttpWebResponse;

									using ( Stream S = HttpResponse.GetResponseStream () ) {}
									//-------------------------------------------------------------
									}
								//-----------------------------------------------------------------
								catch {}
								//-----------------------------------------------------------------
								}, Request_A );
							//---------------------------------------------------------------------
							}
						//-------------------------------------------------------------------------
						catch {}
						//-------------------------------------------------------------------------
						}, http );
					//-----------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------
				catch {}
				//---------------------------------------------------------------------------------
				};
			//-------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------
			AsyncOperationManager.CreateOperation ( null ).PostOperationCompleted 
			                                                                ( Event, PayloadData );
			//-------------------------------------------------------------------------------------
			}
		//*****************************************************************************************
			
		//-----------------------------------------------------------------------------------------
		#endregion
		//*****************************************************************************************
		
		//*****************************************************************************************
		#region // Section des Propriétés
		//-----------------------------------------------------------------------------------------

		//*****************************************************************************************
		/// <summary>
		/// Obtiens ou définit un tableaux de valeurs définies.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public Dictionary<int, string> CustomDimentions { get; private set; }
		//*****************************************************************************************

		//*****************************************************************************************
		/// <summary>
		/// Obtiens la définition de l'écran.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public Size ScreenResolution
			{
			//-------------------------------------------------------------------------------------
			get
				{
				//---------------------------------------------------------------------------------
				int h = (int)Math.Ceiling ( Application.Current.Host.Content.ActualHeight );
				int w = (int)Math.Ceiling ( Application.Current.Host.Content.ActualWidth  );

				return new Size ( h, w );
				//---------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------
			}
		//*****************************************************************************************

		//*****************************************************************************************
		/// <summary>
		/// Obtiens le UserLanguage sourant.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		private static string UserLanguage
			{
			//-------------------------------------------------------------------------------------
			get { return System.Globalization.CultureInfo.CurrentUICulture.Name; }
			//-------------------------------------------------------------------------------------
			}
		//*****************************************************************************************
			
		//-----------------------------------------------------------------------------------------
		#endregion
		//*****************************************************************************************
		}
	//---------------------------------------------------------------------------------------------
	#endregion
	//*********************************************************************************************

	} // Fin du namespace "System.Google"
//*************************************************************************************************

//*************************************************************************************************
// FIN DU FICHIER
//*************************************************************************************************
