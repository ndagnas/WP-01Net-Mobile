﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : FrameworkElementUtils.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet FrameworkElementUtils
// Créé le       : 17/01/2015
// Modifié le    : 17/01/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Microsoft.Xna.Framework.Media;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System.Windows"
//*******************************************************************************************************************************
namespace System.Windows
	{

	//  #####  ####    ###   #   #  #####  #   #   ###   ####   #   #         #####  #      #####
	//  #      #   #  #   #  ## ##  #      #   #  #   #  #   #  #  #          #      #        #  
	//  ###    ####   #####  # # #  ###    #   #  #   #  ####   ###    #####  ###    #        #  
	//  #      #   #  #   #  #   #  #      # # #  #   #  #   #  #  #          #      #        #  
	//  #      #   #  #   #  #   #  #####   # #    ###   #   #  #   #         #####  #####    #  

	//***************************************************************************************************************************
	// Classe FrameworkElementUtils
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit des méthodes utilisées pour manipuler les Frames.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public static class FrameworkElementUtils
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Définit le décalage horizontale de l'objet.
		/// </summary>
		/// <param name="Self">Objet concerné par l'appel.</param>
		/// <param name="Offset">Décallage horizontale.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void SetHorizontalOffset ( this FrameworkElement Self, double Offset )
			{
			//-------------------------------------------------------------------------------------------------------------------
			var T = Self.RenderTransform as TranslateTransform;

			if ( T == null ) Self.RenderTransform = new TranslateTransform () { X = Offset };
			else             T.X                  = Offset;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens le décalage horizontale de l'objet
		/// </summary>
		/// <param name="Self">Objet concerné par l'appel.</param>
		/// <returns>Décallage horizontale.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static Offset GetHorizontalOffset ( this FrameworkElement Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			var T = Self.RenderTransform as TranslateTransform;

			if ( T == null ) { T = new TranslateTransform() { X = 0 }; Self.RenderTransform = T; }

			return new Offset () { Transform = T, Value = T.X };
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens le décalage horizontale de l'objet
		/// </summary>
		/// <param name="Self">Objet concerné par l'appel.</param>
		/// <returns>Décallage horizontale.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static Offset GetVerticalOffset ( this FrameworkElement Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			var T = Self.RenderTransform as TranslateTransform;

			if ( T == null ) { T = new TranslateTransform() { Y = 0 }; Self.RenderTransform = T; }

			return new Offset () { Transform = T, Value = T.Y };
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Anime le décallage de l'objet.
		/// </summary>
		/// <param name="Self">Objet concerné par l'appel.</param>
		/// <param name="From">Origine.</param>
		/// <param name="To">Destination.</param>
		/// <param name="PropertyPath">Nom de la propriété.</param>
		/// <param name="Duration">Durée de l'animation.</param>
		/// <param name="StartTime">Début de l'animation.</param>
		/// <param name="Easing">...</param>
		/// <param name="Completed">Appelé à la fin de l'animation.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void Animate ( this DependencyObject Self, double From, double To, 
		                                         object PropertyPath, int Duration, int StartTime, 
		                                   IEasingFunction Easing = null, Action Completed = null )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Easing == null ) Easing = new SineEase ();

			var Db = new DoubleAnimation ()
				{
				To             = To                                    ,
				From           = From                                  ,
				EasingFunction = Easing                                ,
				Duration       = TimeSpan.FromMilliseconds ( Duration ),
				};
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			Storyboard.SetTarget         ( Db, Self                              );
			Storyboard.SetTargetProperty ( Db, new PropertyPath ( PropertyPath ) );

			var Sb = new Storyboard () { BeginTime = TimeSpan.FromMilliseconds ( StartTime ) };

			if ( Completed != null ) { Sb.Completed += (S, A) => Completed (); }

			Sb.Children.Add ( Db );
			Sb.Begin        (    );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Enregistre une capture de la fenêtre dans la librairie d'image.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void Capture ( this FrameworkElement Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			var Bitmap = new WriteableBitmap ((int) Self.ActualWidth,(int) Self.ActualHeight);

			Bitmap.Render ( Self, null );
			Bitmap.Invalidate ();
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			string TempName = "~FrameworkCapture";
			string FileName = System.IO.Path.GetExtension ( Self.GetType ().ToString () ).Replace ( ".", "" );

			var LocalStore = IsolatedStorageFile.GetUserStoreForApplication ();
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( LocalStore != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				try
					{
					//-----------------------------------------------------------------------------------------------------------
					if ( LocalStore.FileExists ( TempName ) ) LocalStore.DeleteFile ( TempName );
					//-----------------------------------------------------------------------------------------------------------

					//-----------------------------------------------------------------------------------------------------------
					using ( var Fs = LocalStore.CreateFile ( TempName ) )
						{
						Bitmap.SaveJpeg ( Fs, Bitmap.PixelWidth, Bitmap.PixelHeight, 0, 100 );
						}
					//-----------------------------------------------------------------------------------------------------------

					//-----------------------------------------------------------------------------------------------------------
					using ( var Fs = LocalStore.OpenFile ( TempName, FileMode.Open, FileAccess.Read ) )
						{ ( new MediaLibrary () ).SavePicture ( FileName + ".jpg", Fs ); }
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				catch {}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "System.Windows"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
