using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuizAppWeb.Shared
{
	public class Quiz
	{
		private List<Question> _QuizQuestions = new List<Question>();
		private int _QCount = 0;

		/// <summary>
		/// This is the question number up to for the quiz
		/// </summary>
		public int QuestionNumber
		{
			get { return _QCount; }
			set { _QCount = value; }
		}
		private int _Score = 0;

		/// <summary>
		/// This is the score for his quiz
		/// </summary>
		public int Score
		{
			get { return _Score; }
			set { _Score = value; }
		}

		/// <summary>
		/// This tells you how many questions are in the quiz
		/// </summary>
		public int QuestionTotal
		{
			get { return _QuizQuestions.Count; }
		}

		/// <summary>
		/// This tells you how many questions are left in the quiz
		/// </summary>
		public int QuestionsLeft
		{
			get { return _QuizQuestions.Count - (_QCount + 1); }
		}

		/// <summary>
		/// This tells loads the questions.txt file which must be in the bin folder of the project.
		/// </summary>
		public Quiz()
		{ 

			Question q = new Question("﻿What is the capital of France?,London,Paris,Stockport,Marseille,2");
			_QuizQuestions.Add(q);
		}

		/// <summary>
		/// This method will indicate whether the answer number provided is true (correct) or false
		/// if true it will also increment the quiz score by 1.
		/// </summary>
		/// <param name="answer">The answer number between 1 and 4 that you want to check</param>
		/// <returns></returns>
		public bool CheckAnswer(int answer)
		{
			if (answer == _QuizQuestions[_QCount - 1].Correct)
			{
				Score++;
				return true;
			}
			else
			{
				return false;
			}

		}

		/// <summary>
		/// This gets the next question in the quiz. If there are no more questions it will return null
		/// </summary>
		/// <returns>a question object</returns>
		public Question GetNextQuestion()
		{
			if (_QCount < _QuizQuestions.Count)
			{
				_QCount++;
				return _QuizQuestions[_QCount - 1];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// This gets the question in the quiz for the question number specified. If there is no question it will return null
		/// </summary>
		/// <param name="answer">The answer number between 1 and 4 that you want to check</param>
		/// <returns>a question object</returns>
		public Question GetQuestion(int q)
		{
			if (q < _QuizQuestions.Count)
			{

				return _QuizQuestions[q];
			}
			else
			{
				return null;
			}
		}

	}
}

