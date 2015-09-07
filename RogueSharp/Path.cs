﻿using System;
using System.Collections.Generic;

namespace RogueSharp
{
   /// <summary>
   /// A class representing an ordered list of Cells from Start to End
   /// The path can be traversed by the StepForward and StepBackward methods
   /// Implemented by a doubly linked list
   /// </summary>
   public class Path
   {
      private readonly LinkedList<Cell> _steps;
      private LinkedListNode<Cell> _currentStep;

      /// <summary>
      /// Construct a new Path from the specified ordered list of Cells
      /// </summary>
      /// <param name="steps">An IEnumerable of Cells that represent the ordered steps along this Path from Start to End</param>
      /// <exception cref="ArgumentNullException">Thrown when the specified steps parameter is null</exception>
      /// <exception cref="ArgumentException">Thrown when the specified steps parameter is empty</exception>
      public Path( IEnumerable<Cell> steps )
      {
         _steps = new LinkedList<Cell>( steps );

         if ( _steps.Count < 1 )
         {
            throw new ArgumentException( "Path must have steps", "steps" );
         }

         _currentStep = _steps.First;
      }

      /// <summary>
      /// The Cell representing the first step or Start of this Path
      /// </summary>
      public Cell Start
      {
         get
         {
            return _steps.First.Value;
         }
      }

      /// <summary>
      /// The Cell representing the last step or End of this Path
      /// </summary>
      public Cell End
      {
         get
         {
            return _steps.Last.Value;
         }
      }

      /// <summary>
      /// How many steps are in this Path
      /// </summary>
      public int Length
      {
         get
         {
            return _steps.Count;
         }
      }

      /// <summary>
      /// The Cell represeting the step that we are currently on in this Path
      /// </summary>
      public Cell CurrentStep
      {
         get
         {
            return _currentStep.Value;
         }
      }

      /// <summary>
      /// All of the Cells representing the Steps in this Path from Start to End as an IEnumerable
      /// </summary>
      public IEnumerable<Cell> Steps
      {
         get
         {
            return _steps;
         }
      }

      /// <summary>
      /// Move forward along this Path and advance the CurrentStep to the next Step in the Path
      /// </summary>
      /// <returns>A Cell representing the Step that was moved to as we advanced along the Path</returns>
      /// <exception cref="NoMoreStepsException">Thrown when attempting to move forward along a Path on which we are currently at the End</exception>
      public Cell StepForward()
      {
         LinkedListNode<Cell> nextStep = _currentStep.Next;
         if ( nextStep == null )
         {
            throw new NoMoreStepsException( "Cannot take a step foward when at the end of the path" );
         }
         _currentStep = nextStep;
         return nextStep.Value;
      }

      /// <summary>
      /// Move backwards along this Path and rewind the CurrentStep to the previous Step in the Path
      /// </summary>
      /// <returns>A Cell representing the Step that was moved to as we advanced along the Path</returns>
      /// <exception cref="NoMoreStepsException">Thrown when attempting to move backward along a Path on which we are currently at the Start</exception>
      public Cell StepBackward()
      {
         LinkedListNode<Cell> previousStep = _currentStep.Previous;
         if ( previousStep == null )
         {
            throw new NoMoreStepsException( "Cannot take a step backward when at the start of the path" );
         }
         _currentStep = previousStep;
         return previousStep.Value;
      }
   }

   /// <summary>
   /// Exception that is thrown when attempting to move along a Path when there are no more Steps in that direction
   /// </summary>
   public class NoMoreStepsException : Exception
   {
      /// <summary>
      /// Initializes a new instance of the NoMoreStepsException class
      /// </summary>
      public NoMoreStepsException()
      {
      }
      /// <summary>
      /// Initializes a new instance of the NoMoreStepsException class with a specified error message.
      /// </summary>
      /// <param name="message">The error message that explains the reason for the exception.</param>
      public NoMoreStepsException( string message )
         : base( message )
      {
      }
      /// <summary>
      /// Initializes a new instance of the NoMoreStepsException class with a specified error message and a reference to the inner exception that is the cause of this exception.
      /// </summary>
      /// <param name="message">The error message that explains the reason for the exception.</param>
      /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
      public NoMoreStepsException( string message, Exception innerException )
         : base( message, innerException )
      {
      }
   }
}

