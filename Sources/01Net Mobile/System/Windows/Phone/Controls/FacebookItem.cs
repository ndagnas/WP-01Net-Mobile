﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : FacebookItem.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du contrôle FacebookItem
// Créé le       : 02/03/2015
// Modifié le    : 02/03/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.ComponentModel;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System.Windows.Phone.Controls"
//*******************************************************************************************************************************
namespace System.Windows.Phone.Controls
	{

	//   ####   ###   ####   #####  #####  #   #         #   #  #####  #   #  #   #
	//  #      #   #  #   #  #      #      ##  #         ## ##  #      ##  #  #   #
	//   ###   #      ####   ###    ###    # # #  #####  # # #  ###    # # #  #   #
	//      #  #   #  #   #  #      #      #  ##         #   #  #      #  ##  #   #
	//  ####    ###   #   #  #####  #####  #   #         #   #  #####  #   #   ### 

	//***************************************************************************************************************************
	// Classe FacebookItem
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit un objet lien de type menu.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class FacebookItem : System.Windows.Phone.Controls.DelayedButton
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		public static readonly DependencyProperty TitleProperty;
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Constructeur statique de l'objet <b>FacebookItem</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		static FacebookItem ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			TitleProperty = DependencyProperty.Register ( "Title", typeof (string), typeof (FacebookItem), 
				new PropertyMetadata ( string.Empty ) );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>FacebookItem</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public FacebookItem ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			base.DefaultStyleKey = typeof (FacebookItem);

			if ( DesignerProperties.IsInDesignTool ) this.SetValue ( TitleProperty, "@Title" );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
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
