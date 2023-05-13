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
		private List<Question> _QuizQuestions { get; set; }
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

			

			_QuizQuestions = new List<Question>()
			{
				new Question("What is the capital of France?,London,Paris,Stockport,Marseille,2"),
				new Question("When was the great fire of London?,1066,1966,1666,1542,3"),
				new Question("What football team plays at the Etihad Stadium?, Man Utd,Aresenal,Chelsea, Man City,4"),
				new Question("How many points are scored for a try in Rugby Union?,4,5,6,7,2"),
				new Question("What is the name of the longest river in the British Isles?, Thames,Severn,Trent,Shannon,4"),
				new Question("What is the name of the largest planet in the Solar System?,Jupiter,Saturn, Mars, Neptune,1"),
				new Question("Which of these elements are liquid at room temperature?, Water,Mercury,Hydrogen,Phosphorous,2"),
				new Question("When did the World War II end in Europe?,1939,1944,1945,1940,3")
			};
		}

		/// <summary>
		/// This method will indicate whether the answer number provided is true (correct) or false
		/// if true it will also increment the quiz score by 1.
		/// </summary>
		/// <param name="answer">The answer number between 1 and 4 that you want to check</param>
		/// <returns></returns>
		public bool CheckAnswer(int answer)
		{
			if (answer == _QuizQuestions[_QCount].Correct) 
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
		public bool GetNextQuestion()
		{
			if (_QCount < _QuizQuestions.Count)
			{
				_QCount++;

				return false;
				
			}
			else
			{
				return true;
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

