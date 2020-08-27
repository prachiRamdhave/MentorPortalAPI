using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDFAPI.Models
{
    public class CDFGraph
    {
        public string name { get; set; }
        public string userName { get; set; }
        public string age { get; set; }

        // For R
        public string lblBM { get; set; }
        public string lblBL { get; set; }
        public string lblDiffB { get; set; }

        // For A
        public string lblRM { get; set; }
        public string lblRL { get; set; }
        public string lblDiffR { get; set; }
        
        // For P
        public string lblBlM { get; set; }
        public string lblBlL { get; set; }
        public string lblDiffBl { get; set; }


        // For D
        public string lblGM { get; set; }
        public string lblGL { get; set; }
        public string lblDiffG { get; set; }

        //Total

        public string lblTotal { get; set; }

        //RAPD GRAPH 1 information... For Chart1
        public int R1 { get; set; }
        public int A1 { get; set; }
        public int P1 { get; set; }
        public int D1 { get; set; }

        //RAPD GRAPH 2 information... For Chart2
        public int R2 { get; set; }
        public int A2 { get; set; }
        public int P2 { get; set; }
        public int D2 { get; set; }

        //RAPD GRAPH 3 information... For Chart3

        public int R3 { get; set; }
        public int A3 { get; set; }
        public int P3 { get; set; }
        public int D3 { get; set; }


    }

    public class StudentGraph
    {

        public string name { get; set; }
        public string UserName { get; set; }
        public string CandEmailID { get; set; }
        public int CandAge { get; set; }
      

        // For R
        public string lblBM { get; set; }
        public string lblBL { get; set; }
        public string lblDiffB { get; set; }

        // For A
        public string lblRM { get; set; }
        public string lblRL { get; set; }
        public string lblDiffR { get; set; }

        // For P
        public string lblBlM { get; set; }
        public string lblBlL { get; set; }
        public string lblDiffBl { get; set; }


        // For D
        public string lblGM { get; set; }
        public string lblGL { get; set; }
        public string lblDiffG { get; set; }

        //Total

        public string lblTotal { get; set; }

        //RAPD GRAPH 1 information... For Chart1
        public int R1 { get; set; }
        public int A1 { get; set; }
        public int P1 { get; set; }
        public int D1 { get; set; }

        //RAPD GRAPH 2 information... For Chart2
        public int R2 { get; set; }
        public int A2 { get; set; }
        public int P2 { get; set; }
        public int D2 { get; set; }

        //RAPD GRAPH 3 information... For Chart3

        public int R3 { get; set; }
        public int A3 { get; set; }
        public int P3 { get; set; }
        public int D3 { get; set; }


    }
}