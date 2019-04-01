using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPCM_REBUILD
{
    class MarkerTag
    {

        string name;
        string type;
        public MarkerTag(string name, string type) {

            this.name = name;
            this.type = type;
        
        }

        public string Name {

            get { return name; }
            set { name = value; }
        }

        public string Type
        {

            get { return type; }
            set { type = value; }
        }


    }
}
