using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventureCaseStudy
{
    public class IdentifiableObject
    {
        List<string> _identifiers = new List<string>();

        public string FirstId
        {
            get
            {
                if (_identifiers.Count == 0)
                    return "";
                {
                    return _identifiers.First();
                }
            }
        }

        public IdentifiableObject(IEnumerable<string> idents)
        {

            IEnumerable<string> lowercaseIdents = idents.Select(ident => ident.ToLower());
            _identifiers.AddRange(lowercaseIdents);
        }

        public bool AreYou(string id)
        {
            return _identifiers.Contains(id.ToLower());
        }

        public void AddIdentifier(string id)
        {
            _identifiers.Add(id.ToLower());
        }

    }
}
