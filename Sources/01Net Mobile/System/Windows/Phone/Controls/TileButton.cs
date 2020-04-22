﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : TileButton.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du contrôle TileButton
// Créé le       : 23/02/2015
// Modifié le    : 17/11/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Animation;
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
	// Classe TileButton
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit un bouton pour l'écran d'accueil.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class TileButton : System.Windows.Controls.ContentControl
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private bool Initialised = false;
		//-----------------------------------------------------------------------------------------------------------------------
		public static readonly DependencyProperty SourceProperty;
		public static readonly DependencyProperty TitleProperty;
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Constructeur statique de l'objet <b>TileButton</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		static TileButton ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			SourceProperty = DependencyProperty.Register ( "Source", typeof (ImageSource), 
				typeof (TileButton), new PropertyMetadata ( null         ) );
			TitleProperty  = DependencyProperty.Register ( "Title" , typeof (string     ), 
				typeof (TileButton), new PropertyMetadata ( string.Empty ) );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>TileButton</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public TileButton ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			base.DefaultStyleKey = typeof (TileButton);

			if ( DesignerProperties.IsInDesignTool ) this.SetValue ( TitleProperty, "@Title" );
			
			this.Loaded += (S, A) =>
				{
				if ( ! this.Initialised )
					{
					this.Initialised = true;

					var OpenAnimation = GetTemplateChild ( "OpenAnimation" ) as Storyboard;

					if ( OpenAnimation != null ) OpenAnimation.Begin ();
					}
				};
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
		/// Anime l'apparition du bouton.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void AnimateOpen ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			var OpenAnimation = GetTemplateChild ( "OpenAnimation" ) as Storyboard;

			if ( OpenAnimation != null ) OpenAnimation.Begin ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Anime l'apparition du bouton.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void AnimateClose ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			var CloseAnimation = GetTemplateChild ( "CloseAnimation" ) as Storyboard;

			if ( CloseAnimation != null ) CloseAnimation.Begin ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

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
				ButtonControl.Click += (S, A) =>
					{ if ( this.Click != null ) this.Click ( this, A ); };
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
