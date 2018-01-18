using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Tanks.Core
{
    public class SaveLoad
    {

        public Vector2 TankL { get; set; }
        public Vector2 CanonL { get; set; }
        public float CanonLA { get; set; }
        public int hpL { get; set; }
        public float PowerL { get; set; }
        public bool TankStateL { get; set; }
        public bool ShellStateL { get; set; }
        public bool ShellMoveL { get; set; }

        public Vector2 TankR { get; set; }
        public Vector2 CanonR { get; set; }
        public float CanonRA { get; set; }
        public int hpR { get; set; }
        public float PowerR { get; set; }
        public bool TankStateR { get; set; }
        public bool ShellStateR { get; set; }
        public bool ShellMoveR { get; set; }

public SaveLoad() { }
        public void Save(Player Left,Player Right, string path)
        {
            this.TankL = Left.tank.Position;
            this.TankR = Right.tank.Position;
            this.CanonL = Left.canon.Position;
            this.CanonR = Right.canon.Position;
            this.CanonLA = Left.canon.Angle;
            this.CanonRA = Right.canon.Angle;
            this.hpL = Left.HP;
            this.hpR = Right.HP;
            this.PowerL = Left.tmp_power;
            this.PowerR = Right.tmp_power;
            this.TankStateL = Left.TankState;
            this.TankStateR = Right.TankState;
            this.ShellStateL = Left.shell.ShellState;
            this.ShellStateR = Right.shell.ShellState;
            this.ShellMoveL = Left.shell.bmoving;
            this.ShellMoveR = Right.shell.bmoving;

            XmlSerializer xml = new XmlSerializer(typeof(SaveLoad));
            using (TextWriter writer = new StreamWriter(path))
            {
               
                xml.Serialize(writer, this);
            }

        }
        public void Load(Player Left, Player Right, string path)
        {
            SaveLoad tmp;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(typeof(SaveLoad));
                tmp = (SaveLoad)xml.Deserialize(reader);
            }
            Left.tank.Position = tmp.TankL;
            Right.tank.Position = tmp.TankR;
            Left.canon.Position = tmp.CanonL;
            Right.canon.Position = tmp.CanonR;
            Left.canon.Angle = tmp.CanonLA;
            Right.canon.Angle = tmp.CanonRA;
            Left.HP = tmp.hpL;
            Right.HP = tmp.hpR;
            Left.tmp_power = tmp.PowerL;
            Right.tmp_power = tmp.PowerR;
            Left.TankState = tmp.TankStateL;
            Right.TankState = tmp.TankStateR;
            Left.shell.ShellState = tmp.ShellStateL;
            Right.shell.ShellState = tmp.ShellStateR;
            Left.shell.bmoving = tmp.ShellMoveL;
            Right.shell.bmoving = tmp.ShellMoveR;

            if(tmp.ShellMoveR)
            {
                Right.shell.bmoving = false;
                Left.TankState = true;
            }

            if (tmp.ShellMoveL)
            {
                Left.shell.bmoving = false;
                Right.TankState = true;
            }
        }
    }
}
