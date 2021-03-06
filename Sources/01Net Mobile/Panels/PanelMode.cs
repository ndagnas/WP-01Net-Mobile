﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : PanelMode.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet PanelMode
// Créé le       : 17/03/2015
// Modifié le    : 13/05/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "NextRadio.Panels"
//*******************************************************************************************************************************
namespace NextRadio.Panels
	{

	//  ####    ###   #   #  #####  #             #   #   ###   ####   #####
	//  #   #  #   #  ##  #  #      #             ## ##  #   #  #   #  #    
	//  ####   #####  # # #  ###    #      #####  # # #  #   #  #   #  ###  
	//  #      #   #  #  ##  #      #             #   #  #   #  #   #  #    
	//  #      #   #  #   #  #####  #####         #   #   ###   ####   #####

	//***************************************************************************************************************************
	// Enumérateur PanelMode
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Liste les modes d'affichage disponibles pour des Sections.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public enum PanelMode
		{
		/// <summary>
		/// Toutes les sections sont affichées les une au dessous des autres.
		/// </summary>
		Column = 0,
		/// <summary>
		/// Toutes les sections sont affichées sous forme de grille.
		/// </summary>
		Grid   = 1,
		/// <summary>
		/// Toutes les sections sont affichées sous forme de liste.
		/// </summary>
		List   = 2,
		/// <summary>
		/// Toutes les sections sont affichées dans une popup.
		/// </summary>
		Popup  = 3,
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "NextRadio.Panels"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
