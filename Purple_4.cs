namespace Purple_4
{
    public class Purple_4
    {
        public struct Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _timeRecorded;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;
            public bool TimeRecorded => _timeRecorded;

            public Sportsman(string name, string surname)
            {
                this._name = name;
                this._surname = surname;
                this._time = 0;
                this._timeRecorded = false;
            }

            public void Run(double time)
            {
                if (this._timeRecorded)
                    return;
                if (time < 0)
                    return;
                this._time = time;
                this._timeRecorded = true;
            }
        }

        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] copy = new Sportsman[_sportsmen.Length];
                    Array.Copy(_sportsmen, copy, _sportsmen.Length);
                    return copy;
                }
            }

            public Group(string name)
            {
                this._name = name;
                this._sportsmen = new Sportsman[] { };
            }

            public Group(Group otherGroup)
            {
                this._name = otherGroup.Name;
                this._sportsmen = new Sportsman[] { };
                Array.Copy(otherGroup.Sportsmen, this._sportsmen, otherGroup.Sportsmen.Length);
            }

            public void Add(Sportsman sportsman)
            {
                Sportsman[] oldArray = this.Sportsmen;
                this._sportsmen = new Sportsman[oldArray.Length + 1];
                Array.Copy(oldArray, this._sportsmen, oldArray.Length);
                this._sportsmen[oldArray.Length] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null)
                    return;

                Sportsman[] oldArray = this.Sportsmen;
                this._sportsmen = new Sportsman[oldArray.Length + sportsmen.Length];
                oldArray.CopyTo(this._sportsmen, 0);
                sportsmen.CopyTo(this._sportsmen, oldArray.Length);
            }

            public void Add(Group otherGroup)
            {
                Sportsman[] oldArray = this.Sportsmen;
                Sportsman[] otherArrray = otherGroup.Sportsmen;

                this._sportsmen = new Sportsman[oldArray.Length + otherArrray.Length];

                oldArray.CopyTo(this._sportsmen, 0);
                otherArrray.CopyTo(this._sportsmen, oldArray.Length);
            }

            public void Sort()
            {
                if (this.Sportsmen.Any(s => !s.TimeRecorded))
                    return;

                Sportsman[] sorted = this.Sportsmen.OrderBy(s => s.Time).ToArray();
            }

            public static Group Merge(Group group1, Group group2)
            {
                Sportsman[] array1 = group1.Sportsmen;
                Sportsman[] array2 = group2.Sportsmen;

                Sportsman[] newArray = new Sportsman[array1.Length + array2.Length];

                int i1 = 0;
                int i2 = 0;
                int iTotal = 0;

                while (iTotal < newArray.Length)
                {
                    if (array1[i1].Time <= array2[i2].Time)
                    {
                        newArray[iTotal++] = array1[i1++];
                    }
                    else
                    {
                        newArray[iTotal++] = array2[i2++];
                    }
                }

                Group newGroup = new Group("Финалисты");
                newGroup.Add(newArray);

                return newGroup;
            }
        }
    }
}
