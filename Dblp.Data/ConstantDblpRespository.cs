using Dblp.Data.Interfaces;

namespace Dblp.Data
{
    public class ConstantDblpRespository : IDblpBibTexRepository
    {
        public string GetBibTex(string dblpKey)
        {

            var contentOfSingleBibTeXFile = @"@article{DBLP:journals/jsc/Fortenbacher87,
  author    = {Albrecht Fortenbacher},
  title     = {An Algebraic Approach to Unification Under Associativity and Commutativity},
  journal   = {J. Symb. Comput.},
  volume    = {3},
  number    = {3},
  pages     = {217--229},
  year      = {1987},
  url       = {http://dx.doi.org/10.1016/S0747-7171(87)80001-9},
  doi       = {10.1016/S0747-7171(87)80001-9},
  timestamp = {Tue, 20 Sep 2011 11:17:55 +0200},
  biburl    = {http://dblp.uni-trier.de/rec/bib/journals/jsc/Fortenbacher87},
  bibsource = {dblp computer science bibliography, http://dblp.org}
}

";
            return contentOfSingleBibTeXFile;
        }
    }
}
