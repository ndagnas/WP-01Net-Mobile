﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : FacebookTile.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du contrôle FacebookTile
// Créé le       : 23/02/2015
// Modifié le    : 09/05/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System.Windows.Phone.Controls"
//*******************************************************************************************************************************
namespace System.Windows.Phone.Controls
	{

	//  #####  #  #      #####         ####   #   #  #####  #####   ###   #   #
	//    #    #  #      #             #   #  #   #    #      #    #   #  ##  #
	//    #    #  #      ###    #####  ####   #   #    #      #    #   #  # # #
	//    #    #  #      #             #   #  #   #    #      #    #   #  #  ##
	//    #    #  #####  #####         ####    ###     #      #     ###   #   #

	//***************************************************************************************************************************
	// Classe FacebookTile
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit un bouton pour l'écran d'accueil.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class FacebookTile : System.Windows.Controls.ContentControl
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		public static readonly DependencyProperty SourceProperty;
		public static readonly DependencyProperty TitleProperty;
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Constructeur statique de l'objet <b>FacebookTile</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		static FacebookTile ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			SourceProperty = DependencyProperty.Register ( "Source", typeof (ImageSource),
				typeof (FacebookTile), new PropertyMetadata ( null         ) );
			TitleProperty  = DependencyProperty.Register ( "Title" , typeof (string     ), 
				typeof (FacebookTile), new PropertyMetadata ( string.Empty ) );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>FacebookTile</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public FacebookTile ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			base.DefaultStyleKey = typeof (FacebookTile);

			if ( DesignerProperties.IsInDesignTool ) this.SetValue ( TitleProperty, "@Title" );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // Section des Procédures
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// S'assure de l'application du template actuel.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public override void OnApplyTemplate ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			base.OnApplyTemplate ();
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			var ButtonControl = GetTemplateChild ( "ButtonControl" ) as DelayedButton;

			if ( ButtonControl != null )
				{
				ButtonControl.Click += (S, A) => { if ( this.Click != null ) this.Click ( this, A ); };
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lorsque l'utilisateur clique sur un Button
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public event RoutedEventHandler Click;
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtient ou définit le <b>ImageSource</b> de l'image. 
		/// </summary>
		/// <returns>URI du fichier image.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public ImageSource Source
			{
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			get { return (ImageSource)base.GetValue ( SourceProperty ); }
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			set { base.SetValue ( SourceProperty, value ); }
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens ou définit le texte du lien.
		/// </summary>
		/// <returns>Chaine.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public string Title
			{
			//-------------------------------------------------------------------------------------------------------------------
			get { return (string)base.GetValue ( TitleProperty ); }
			//-------------------------------------------------------------------------------------------------------------------
			set { base.SetValue ( TitleProperty, value ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "System.Windows.Phone.Controls"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
