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
        int _position;

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
        [Browsable(false)]
		public Group AssignedGroup
		{
			get { return _group; }
            set { _group = value; }
		}

        [DisplayName("Группа")]
        public int AssignedGroupNo
        {
            get { return _group == null ? 0 : _group.Id; }
        }

        [DisplayName("Номер пары")]
        public int Position
        {
            get { return _position == null ? 0 :_position; }
            set { _position = value; }
        }


        internal void AssignGroup(int groupNo)
        {
            if (groupNo == 0)
            {
                _group = null;
                return;
            }

            Group group;
            if (Model.Groups.Any(g => g.Id == groupNo))
            {
                group = Model.Groups.First(g => g.Id == groupNo);
            }
            else
            {
                group = new Group {Id = groupNo };
            }
            group.Players.Add(this);
            _group = group;
        }

    }


}
