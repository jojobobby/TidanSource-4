﻿namespace wServer.realm.entities
{
    public abstract class Character : Entity
    {





        public Character(RealmManager manager, ushort objType, wRandom rand)
            : base(manager, objType)
        {
            Random = rand;

            _hp = new SV<int>(this, StatsType.HP, 0);
            _maximumHP = new SV<int>(this, StatsType.MaximumHP, 0);


            if (ObjectDesc == null) return;
            Name = ObjectDesc.DisplayId ?? "";
            if (ObjectDesc.SizeStep != 0)
            {
                int step = Random.Next(0, (ObjectDesc.MaxSize - ObjectDesc.MinSize) / ObjectDesc.SizeStep + 1) *
                           ObjectDesc.SizeStep;
                Size = ObjectDesc.MinSize + step;
            }
            else
                Size = ObjectDesc.MinSize;

            HP = (int)ObjectDesc.MaxHP;
        }

        public int MaximumHP
        {
            get { return _maximumHP.GetValue(); }
            set { _maximumHP.SetValue(value); }
        }

        public int HP
        {
            get { return _hp.GetValue(); }
            set { _hp.SetValue(value); }
        }

        public wRandom Random { get; private set; }

        private readonly SV<int> _hp;
        private readonly SV<int> _maximumHP;
    }
}