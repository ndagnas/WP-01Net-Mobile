﻿//*************************************************************************************************
// DEBUT DU FICHIER
//*************************************************************************************************

//*************************************************************************************************
// Nom           : CompressionLevel.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet CompressionLevel
// Environnement : Visual Studio 2012
// Créé le       : 19/04/2015
// Modifié le    : 19/04/2015
//*************************************************************************************************

//-------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------
using System;
//-------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------

//*************************************************************************************************
// Début du bloc "System.IO.Compression"
//*************************************************************************************************
namespace System.IO.Compression
	{

	//*********************************************************************************************
	// Classe CompressionLevel
	//*********************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------
	public enum CompressionLevel
		{
		BestCompression = 9,
		BestSpeed = 1,
		Default = 6,
		Level0 = 0,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
		Level4 = 4,
		Level5 = 5,
		Level6 = 6,
		Level7 = 7,
		Level8 = 8,
		Level9 = 9,
		None = 0
		}
	//---------------------------------------------------------------------------------------------
	#endregion
	//*********************************************************************************************

	} // Fin du namespace "System.IO.Compression"
//*************************************************************************************************

//*************************************************************************************************
// FIN DU FICHIER
//*************************************************************************************************
