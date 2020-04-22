﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : LeftPanelColumnMode.xaml.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de la Page LeftPanelColumnMode
// Créé le       : 17/03/2015
// Modifié le    : 19/05/2018
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Phone.Controls;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Microsoft.Phone.Shell;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using NextRadio.Service;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "NextRadio.Panels"
//*******************************************************************************************************************************
namespace NextRadio.Panels
	{

	//  #      #####  #####  #####         ####    ###   #   #  #####  #    
	//  #      #      #        #           #   #  #   #  ##  #  #      #    
	//  #      ###    ###      #    #####  ####   #####  # # #  ###    #    
	//  #      #      #        #           #      #   #  #  ##  #      #    
	//  #####  #####  #        #           #      #   #  #   #  #####  #####

	//***************************************************************************************************************************
	// Contrôle LeftPanelColumnMode
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Définit la page permettant l'identification.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public partial class LeftPanelColumnMode : UserControl
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>LeftPanelColumnMode</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public LeftPanelColumnMode ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.InitializeComponent ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Est appelé lors d'un clic sur un lien interne.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnSectionClick ( object Sender, RoutedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			FrameworkElement Self = Sender as FrameworkElement;

			if ( Self != null && this.Click != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				SectionType Section = SectionType.All;

				switch ( Self.Tag.ToString () )
					{
					case "SectionType.Telecoms"   : Section = SectionType.Telecoms;   break;
					case "SectionType.Products"   : Section = SectionType.Products;   break;
					case "SectionType.Technology" : Section = SectionType.Technology; break;
					case "SectionType.Apps"       : Section = SectionType.Apps;       break;
					case "SectionType.Security"   : Section = SectionType.Security;   break;
					case "SectionType.Politics"   : Section = SectionType.Politics;   break;
					case "SectionType.Culture"    : Section = SectionType.Culture;    break;
					case "SectionType.Science"    : Section = SectionType.Science;    break;
					case "SectionType.Society"    : Section = SectionType.Society;    break;
					case "SectionType.Games"      : Section = SectionType.Games;      break;
					case "SectionType.Rugby"      : Section = SectionType.Rugby;      break;
					case "SectionType.Tv"         : Section = SectionType.Tv;         break;
					case "SectionType.Bookmarks"  : Section = SectionType.Bookmarks;  break;
					}
				
				this.Click ( this, new ObjectEventArgs<SectionType> ( Section ) );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'un clic sur un élément de la page.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public event EventHandler Click;
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "NextRadio.Panels"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
