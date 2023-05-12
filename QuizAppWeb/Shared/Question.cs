using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppWeb.Shared
{
	public class Question
	{
		private string _Q;
		/// <summary>
		/// The Question to be asked
		/// </summary>
		public string QuestionText
		{
			get { return _Q; }
			set { _Q = value; }
		}
		private string _A1;
		/// <summary>
		/// The first possible answer
		/// </summary>
		public string Answer1
		{
			get { return _A1; }
			set { _A1 = value; }
		}
		private string _A2;
		/// <summary>
		/// The second possible answer
		/// </summary>
		public string Answer2
		{
			get { return _A2; }
			set { _A2 = value; }
		}
		private string _A3;
		/// <summary>
		/// The third possible answer
		/// </summary>
		public string Answer3
		{
			get { return _A3; }
			set { _A3 = value; }
		}
		private string _A4;
		/// <summary>
		/// The fourth possible answer
		/// </summary>
		public string Answer4
		{
			get { return _A4; }
			set { _A4 = value; }
		}
		private int _correct;
		/// <summary>
		/// The correct answer as a number 1-4.
		/// </summary>
		public int Correct
		{
			get { return _correct; }
			set { _correct = value; }
		}



		public Question(string stringin) //constructor
		{
			string[] DataIn = stringin.Split(',');
			_Q = DataIn[0];
			_A1 = DataIn[1];
			_A2 = DataIn[2];
			_A3 = DataIn[3];
			_A4 = DataIn[4];
			_correct = int.Parse(DataIn[5]);
		}
	}
}
