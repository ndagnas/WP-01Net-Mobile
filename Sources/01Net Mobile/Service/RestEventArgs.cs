﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : RestEventArgs.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet RestEventArgs
// Créé le       : 18/02/2015
// Modifié le    : 28/06/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "NextRadio.Service"
//*******************************************************************************************************************************
namespace NextRadio.Service
	{

	//  ####   #####   ####  #####         #####  #   #  #####          ###   ####    ###     ####
	//  #   #  #      #        #           #      #   #    #           #   #  #   #  #   #   #    
	//  ####   ###     ###     #    #####  ###    #   #    #    #####  #####  ####   #        ### 
	//  #   #  #          #    #           #       # #     #           #   #  #   #  #   ##      #
	//  #   #  #####  ####     #           #####    #      #           #   #  #   #   ### #  #### 

	//***************************************************************************************************************************
	// Classe RestEventArgs
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit des données pour l'événement <b>RestEventHandler</b>.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class RestEventArgs : EventArgs
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>RestEventArgs</b>.
		/// </summary>
		/// <param name="AssociateObject">Objet associé.</param>
		/// <param name="Content">Statut de la demande.</param>
		/// <param name="Delay">Temps de chargement du contenue.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public RestEventArgs ( object AssociateObject, string Content, RestRequestResult Result )
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.AssociateObject = AssociateObject;
			this.Content         = Content;
			this.Result          = Result;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>RestEventArgs</b>.
		/// </summary>
		/// <param name="AssociateObject">Objet associé.</param>
		/// <param name="Content">Contenue demandé.</param>
		/// <param name="Delay">Temps de chargement du contenue.</param>
		/// <param name="Validity">Age du contenue.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public RestEventArgs ( object AssociateObject, string Content )
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.AssociateObject = AssociateObject;
			this.Content         = Content;
			this.Result          = RestRequestResult.Success;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtient l'objet associé.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public object AssociateObject { get; private set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtient le contenue demandé.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public string Content { get; private set; }
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtient statut de la demande.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public RestRequestResult Result { get; private set; }
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "NextRadio.Service"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************