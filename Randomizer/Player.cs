using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Randomizer
{
	class Player
	{
		int _id;
		string _name;
		int _score;
		Group _group;
		public double random;

		[DisplayName("ID")]
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		[DisplayName("Имя игрока")]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[DisplayName("Очки")]
		public int Score
		{
			get { return _score; }
			set { _score = value; }
		}

		[DisplayName("Группа")]
		public Group AssignedGroup
		{
			get { return _group; }
			set { _group = value; }
		}

	}
}
