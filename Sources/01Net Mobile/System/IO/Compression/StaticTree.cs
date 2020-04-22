﻿//*************************************************************************************************
// DEBUT DU FICHIER
//*************************************************************************************************

//*************************************************************************************************
// Nom           : StaticTree.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet StaticTree
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
	// Classe StaticTree
	//*********************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------
    internal sealed class StaticTree
    {
        internal static readonly StaticTree BitLengths = new StaticTree(null, extra_blbits, 0, InternalConstants.BL_CODES, InternalConstants.MAX_BL_BITS);
        internal static readonly StaticTree Distances = new StaticTree(distTreeCodes, DeflateManager.ExtraDistanceBits, 0, InternalConstants.D_CODES, InternalConstants.MAX_BITS);
        internal static readonly short[] distTreeCodes = new short[] { 
            0, 5, 0x10, 5, 8, 5, 0x18, 5, 4, 5, 20, 5, 12, 5, 0x1c, 5, 
            2, 5, 0x12, 5, 10, 5, 0x1a, 5, 6, 5, 0x16, 5, 14, 5, 30, 5, 
            1, 5, 0x11, 5, 9, 5, 0x19, 5, 5, 5, 0x15, 5, 13, 5, 0x1d, 5, 
            3, 5, 0x13, 5, 11, 5, 0x1b, 5, 7, 5, 0x17, 5
         };
        internal int elems;
        internal static readonly int[] extra_blbits = new int[] { 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            2, 3, 7
         };
        internal int extraBase;
        internal int[] extraBits;
        internal static readonly short[] lengthAndLiteralsTreeCodes = new short[] { 
            12, 8, 140, 8, 0x4c, 8, 0xcc, 8, 0x2c, 8, 0xac, 8, 0x6c, 8, 0xec, 8, 
            0x1c, 8, 0x9c, 8, 0x5c, 8, 220, 8, 60, 8, 0xbc, 8, 0x7c, 8, 0xfc, 8, 
            2, 8, 130, 8, 0x42, 8, 0xc2, 8, 0x22, 8, 0xa2, 8, 0x62, 8, 0xe2, 8, 
            0x12, 8, 0x92, 8, 0x52, 8, 210, 8, 50, 8, 0xb2, 8, 0x72, 8, 0xf2, 8, 
            10, 8, 0x8a, 8, 0x4a, 8, 0xca, 8, 0x2a, 8, 170, 8, 0x6a, 8, 0xea, 8, 
            0x1a, 8, 0x9a, 8, 90, 8, 0xda, 8, 0x3a, 8, 0xba, 8, 0x7a, 8, 250, 8, 
            6, 8, 0x86, 8, 70, 8, 0xc6, 8, 0x26, 8, 0xa6, 8, 0x66, 8, 230, 8, 
            0x16, 8, 150, 8, 0x56, 8, 0xd6, 8, 0x36, 8, 0xb6, 8, 0x76, 8, 0xf6, 8, 
            14, 8, 0x8e, 8, 0x4e, 8, 0xce, 8, 0x2e, 8, 0xae, 8, 110, 8, 0xee, 8, 
            30, 8, 0x9e, 8, 0x5e, 8, 0xde, 8, 0x3e, 8, 190, 8, 0x7e, 8, 0xfe, 8, 
            1, 8, 0x81, 8, 0x41, 8, 0xc1, 8, 0x21, 8, 0xa1, 8, 0x61, 8, 0xe1, 8, 
            0x11, 8, 0x91, 8, 0x51, 8, 0xd1, 8, 0x31, 8, 0xb1, 8, 0x71, 8, 0xf1, 8, 
            9, 8, 0x89, 8, 0x49, 8, 0xc9, 8, 0x29, 8, 0xa9, 8, 0x69, 8, 0xe9, 8, 
            0x19, 8, 0x99, 8, 0x59, 8, 0xd9, 8, 0x39, 8, 0xb9, 8, 0x79, 8, 0xf9, 8, 
            5, 8, 0x85, 8, 0x45, 8, 0xc5, 8, 0x25, 8, 0xa5, 8, 0x65, 8, 0xe5, 8, 
            0x15, 8, 0x95, 8, 0x55, 8, 0xd5, 8, 0x35, 8, 0xb5, 8, 0x75, 8, 0xf5, 8, 
            13, 8, 0x8d, 8, 0x4d, 8, 0xcd, 8, 0x2d, 8, 0xad, 8, 0x6d, 8, 0xed, 8, 
            0x1d, 8, 0x9d, 8, 0x5d, 8, 0xdd, 8, 0x3d, 8, 0xbd, 8, 0x7d, 8, 0xfd, 8, 
            0x13, 9, 0x113, 9, 0x93, 9, 0x193, 9, 0x53, 9, 0x153, 9, 0xd3, 9, 0x1d3, 9, 
            0x33, 9, 0x133, 9, 0xb3, 9, 0x1b3, 9, 0x73, 9, 0x173, 9, 0xf3, 9, 0x1f3, 9, 
            11, 9, 0x10b, 9, 0x8b, 9, 0x18b, 9, 0x4b, 9, 0x14b, 9, 0xcb, 9, 0x1cb, 9, 
            0x2b, 9, 0x12b, 9, 0xab, 9, 0x1ab, 9, 0x6b, 9, 0x16b, 9, 0xeb, 9, 0x1eb, 9, 
            0x1b, 9, 0x11b, 9, 0x9b, 9, 0x19b, 9, 0x5b, 9, 0x15b, 9, 0xdb, 9, 0x1db, 9, 
            0x3b, 9, 0x13b, 9, 0xbb, 9, 0x1bb, 9, 0x7b, 9, 0x17b, 9, 0xfb, 9, 0x1fb, 9, 
            7, 9, 0x107, 9, 0x87, 9, 0x187, 9, 0x47, 9, 0x147, 9, 0xc7, 9, 0x1c7, 9, 
            0x27, 9, 0x127, 9, 0xa7, 9, 0x1a7, 9, 0x67, 9, 0x167, 9, 0xe7, 9, 0x1e7, 9, 
            0x17, 9, 0x117, 9, 0x97, 9, 0x197, 9, 0x57, 9, 0x157, 9, 0xd7, 9, 0x1d7, 9, 
            0x37, 9, 0x137, 9, 0xb7, 9, 0x1b7, 9, 0x77, 9, 0x177, 9, 0xf7, 9, 0x1f7, 9, 
            15, 9, 0x10f, 9, 0x8f, 9, 0x18f, 9, 0x4f, 9, 0x14f, 9, 0xcf, 9, 0x1cf, 9, 
            0x2f, 9, 0x12f, 9, 0xaf, 9, 0x1af, 9, 0x6f, 9, 0x16f, 9, 0xef, 9, 0x1ef, 9, 
            0x1f, 9, 0x11f, 9, 0x9f, 9, 0x19f, 9, 0x5f, 9, 0x15f, 9, 0xdf, 9, 0x1df, 9, 
            0x3f, 9, 0x13f, 9, 0xbf, 9, 0x1bf, 9, 0x7f, 9, 0x17f, 9, 0xff, 9, 0x1ff, 9, 
            0, 7, 0x40, 7, 0x20, 7, 0x60, 7, 0x10, 7, 80, 7, 0x30, 7, 0x70, 7, 
            8, 7, 0x48, 7, 40, 7, 0x68, 7, 0x18, 7, 0x58, 7, 0x38, 7, 120, 7, 
            4, 7, 0x44, 7, 0x24, 7, 100, 7, 20, 7, 0x54, 7, 0x34, 7, 0x74, 7, 
            3, 8, 0x83, 8, 0x43, 8, 0xc3, 8, 0x23, 8, 0xa3, 8, 0x63, 8, 0xe3, 8
         };
        internal static readonly StaticTree Literals = new StaticTree(lengthAndLiteralsTreeCodes, DeflateManager.ExtraLengthBits, InternalConstants.LITERALS + 1, InternalConstants.L_CODES, InternalConstants.MAX_BITS);
        internal int maxLength;
        internal short[] treeCodes;

        private StaticTree(short[] treeCodes, int[] extraBits, int extraBase, int elems, int maxLength)
        {
            this.treeCodes = treeCodes;
            this.extraBits = extraBits;
            this.extraBase = extraBase;
            this.elems = elems;
            this.maxLength = maxLength;
        }
    }
	//---------------------------------------------------------------------------------------------
	#endregion
	//*********************************************************************************************

	} // Fin du namespace "System.IO.Compression"
//*************************************************************************************************

//*************************************************************************************************
// FIN DU FICHIER
//*************************************************************************************************

