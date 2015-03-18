namespace Zakar.ViewModels
{
    public class PartnershipArmRankingViewModel
    {
        public int FirstArmId { get; set; }

        public PartnershipArmViewModel FirstPartnershipArm { get; set; }

        public int SecondArmId { get; set; }

        public PartnershipArmViewModel SecondPartnershipArm { get; set; }

        public int ThirdArmId { get; set; }

        public PartnershipArmViewModel ThirdPartnershipArm { get; set; }
    }
}