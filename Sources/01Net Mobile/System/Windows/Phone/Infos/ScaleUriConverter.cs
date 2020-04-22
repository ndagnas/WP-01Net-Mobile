//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : ScaleUriConverter.cs
// Auteur        : Nicolas Dagnas
// Description   : Impl�mentation de l'objet ScaleUriConverter
// Cr�� le       : 23/02/2015
// Modifi� le    : 23/02/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Windows.Data;
using System.Globalization;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// D�but du bloc "System.Windows.Phone.Infos"
//*******************************************************************************************************************************
namespace System.Windows.Phone.Infos
	{

	//   ####   ###    ###   #      #####         #   #  ####   #
	//  #      #   #  #   #  #      #             #   #  #   #  #
	//   ###   #      #####  #      ###    #####  #   #  ####   #
	//      #  #   #  #   #  #      #             #   #  #   #  #
	//  ####    ###   #   #  #####  #####          ###   #   #  #

	//***************************************************************************************************************************
	// Classe ScaleUri & ScaleUriConverter
	//***************************************************************************************************************************
	#region // D�claration et Impl�mentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Permet de g�rer les diff�rentes d�finitions.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class ScaleUriConverter : IValueConverter
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private static string CurrentRessString = string.Empty;
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Constructeur statique de l'objet <b>ScaleFactor</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		static ScaleUriConverter ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			switch ( DeviceInfos.ScaleFactor )
				{
				//-----------------------------------------------------------------------------------------------------------
				case Factor.Scale_WVGA  : CurrentRessString = "wvga" ; break;
				case Factor.Scale_720P  : CurrentRessString = "720p" ; break;
				case Factor.Scale_WXGA  : CurrentRessString = "wxga" ; break;
				case Factor.Scale_1080P : CurrentRessString = "1080p"; break;
				default                 : CurrentRessString = "wvga" ; break;
				//-----------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Convertit le chemin standart en chemin li� � la d�finition courante.
		/// </summary>
		/// <param name="UriString">Lien � convertir.</param>
		/// <returns>Lien convertis.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string Convert ( string UriString )
			{
			//-------------------------------------------------------------------------------------------------------------------
			string Extention = Path.GetExtension ( UriString );

			return UriString.Replace ( Extention, string.Format ( "-{0}{1}", CurrentRessString, Extention ) );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Modifie les donn�es sources avant de les passer � la cible en vue de leur affichage 
		/// dans l'interface utilisateur.
		/// </summary>
		/// <param name="Value">Les donn�es sources pass�es � la cible.</param>
		/// <param name="TargetType">
		/// Type de donn�es attendu par la propri�t� de d�pendance cible.
		/// </param>
		/// <param name="Parameter">
		/// Un param�tre optionnel � utiliser dans la logique du convertisseur.
		/// </param>
		/// <param name="Culture">La culture de la conversion.</param>
		/// <returns>La valeur � passer � la propri�t� de d�pendance cible.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public object Convert ( object Value, Type TargetType, object Parameter, CultureInfo Culture )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Value is string ) Value = Convert ( (string)Value );
		
			return Value;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Modifie les donn�es cibles avant de les passer � l'objet source.
		/// </summary>
		/// <param name="Value">Les donn�es cibles pass�es � la source.</param>
		/// <param name="TargetType">Type de donn�es attendu par l'objet source.</param>
		/// <param name="Parameter">
		/// Un param�tre optionnel � utiliser dans la logique du convertisseur.
		/// </param>
		/// <param name="Culture">La culture de la conversion.</param>
		/// <returns>La valeur � passer � l'objet source.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public object ConvertBack ( object Value, Type TargetType, object Parameter, CultureInfo Culture )
			{ throw new NotSupportedException (); }
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "System.Windows.Phone.Infos"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
