﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : SortedArticles.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet SortedArticles
// Créé le       : 20/04/2015
// Modifié le    : 11/08/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "NextRadio.Service"
//*******************************************************************************************************************************
namespace NextRadio.Service
	{

	//   ####   ###   ####   #####  #####  ####           ###   ####   #####  #   ###   #    
	//  #      #   #  #   #    #    #      #   #         #   #  #   #    #    #  #   #  #    
	//   ###   #   #  ####     #    ###    #   #  #####  #####  ####     #    #  #      #    
	//      #  #   #  #   #    #    #      #   #         #   #  #   #    #    #  #   #  #    
	//  ####    ###   #   #    #    #####  ####          #   #  #   #    #    #   ###   #####  ##
	
	//***************************************************************************************************************************
	// Classe SortedArticles
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Contistue une liste d'article triés.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class SortedArticles : IList<Article>
		{
		//***********************************************************************************************************************
		#region // Classe Sorter
		//-----------------------------------------------------------------------------------------------------------------------
		class Sorter : IComparer<Article>
			{
			//-------------------------------------------------------------------------------------------------------------------
			public int Compare ( Article A, Article B )
				{
				//---------------------------------------------------------------------------------------------------------------
				if (   A.IsHeadline && ! B.IsHeadline ) return -1;
				if ( ! A.IsHeadline &&   B.IsHeadline ) return  1;
				if (   A.IsHeadline &&   B.IsHeadline ) return  0;
				
				int Result = B.LastModified.CompareTo ( A.LastModified );

				return ( Result == 0 ) ? B.Title.CompareTo ( A.Title ) : Result;
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private List<Article> FItems = new List<Article> ();
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		#region // Section des Procédures Statiques
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Crée une nouvelle instance de l'objet <b>SortedArticles</b> et l'initialise avec Token.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public static SortedArticles FromJSON ( JToken Token )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return new SortedArticles ().Combine ( Token );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Procédures Publiques
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Ajoute un objet à la fin de l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Add ( Article Item )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Item != null && ! this.Contains ( Item ) ) this.FItems.Add ( Item );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Supprime tous les éléments de l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Clear () { this.FItems.Clear (); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Ajoute les éléments contenues dans Token à l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public SortedArticles Combine ( JToken Token )
			{
			//-------------------------------------------------------------------------------------------------------------------
			while ( Token != null )
				{ this.Add ( Article.Parse ( Token ) ); Token = Token.Next; }

			return this;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Détermine si un élément est dans l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool Contains ( Article Article )
			{
			//-------------------------------------------------------------------------------------------------------------------
			foreach ( Article Item in this )
				{ if ( Item.ArticleID.Equals ( Article.ArticleID ) ) return true; }

			return false;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Non supporté.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void CopyTo ( Article[] A, int B ) { throw new NotImplementedException (); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Retourne un énumérateur qui itère au sein de l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public IEnumerator<Article> GetEnumerator () { return this.FItems.GetEnumerator (); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Recherche l'objet spécifié et retourne l'index de base zéro de la première occurrence 
		/// dans l'ensemble de l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public int IndexOf ( Article item ) { return this.FItems.IndexOf ( item ); }
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Non supporté.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Insert ( int index, Article item ) { throw new NotImplementedException (); }
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Non supporté.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool Remove ( Article A ) { throw new NotImplementedException (); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Non supporté.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void RemoveAt ( int index ) { throw new NotImplementedException (); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Trie l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public SortedArticles Sort () { this.FItems.Sort ( new Sorter () ); return this; }
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // Section des Procédures IEnumerable
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Retourne un énumérateur qui itère au sein de l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		IEnumerator System.Collections.IEnumerable.GetEnumerator ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			return this.FItems.GetEnumerator ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtient ou définit l'élément dans la position d'index spécifiée.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public Article this[int Index]
			{ get { return this.FItems[Index]; } set { this.FItems[Index] = value; } }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtient le nombre d'éléments réellement contenus dans l'objet courant.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public int Count { get { return this.FItems.Count; } }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// 
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool IsReadOnly { get { return false; } }
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
