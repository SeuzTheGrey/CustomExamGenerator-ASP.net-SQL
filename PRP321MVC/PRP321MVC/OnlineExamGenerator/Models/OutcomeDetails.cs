using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExamGenerator.Models
{
    public class OutcomeDetails : Outcome
    {
        private int outcomeDetailsId;
        private string material;

        #region Properties & Constructors
        public OutcomeDetails()
            : base()
        {
            OutcomeDetailsId = 0;
            Material = "";
        }

        public OutcomeDetails(int cOutcomeDetailsId)
            : base()
        {
            OutcomeDetailsId = cOutcomeDetailsId;
            Material = "";
        }

        public OutcomeDetails(int cOutcomeId, string cMaterial)
            : base(cOutcomeId)
        {
            OutcomeDetailsId = 0;
            Material = cMaterial;
        }

        internal OutcomeDetails(int cOutcomeDetailsId, int cOutcomeId, string cMaterial)
            : base(cOutcomeId)
        {
            OutcomeDetailsId = cOutcomeDetailsId;
            Material = cMaterial;
        }

        public OutcomeDetails(int cOutcomeDetailsId, int cOutcomeId, string cMaterial, string cOutcomeName)
            : base(cOutcomeId, cOutcomeName)
        {
            OutcomeDetailsId = cOutcomeDetailsId;
            Material = cMaterial;
        }

        public string Material
        {
            get { return material; }
            set { material = value; }
        }

        public int OutcomeDetailsId
        {
            get { return outcomeDetailsId; }
            set { outcomeDetailsId = value; }
        }
        #endregion
    }
}