namespace Footballize.Common
{
    public class GlobalConstants
    {
        public const string CanCreateGathersRoleName = "CanCreateGathers";
        public const string CanEditGathersRoleName = "CanEditGathers";
        public const string CanDeleteGathersRoleName = "CanDeleteGathers";
        public const string CanCreateRecruitmentRoleName = "CanCreateRecruitment";
        public const string CanEditRecruitmentRoleName = "CanEditRecruitment";
        public const string CanDeleteRecruitmentRoleName = "CanDeleteRecruitment";
        public const string CanSeeAdminAreaRoleName = "CanSeeAdminArea";
        public const string CanBanPlayers = "CanBanPlayers";

        public const string RequiredNumberOfPlayersNotReachedErrorMessage = "Cannot start this game because the required number of players is not reached yet!";
        public const string ThisGameIsAlreadyStartedErrorMessage = "Cannot start this game because its already started!";
        public const string ThisGameIsNotStartedYetErrorMessage = "Cannot complete game witch is not started!";
        public const string KickPlayerOnlyInRegistrationModeErrorMessage = "You can manage players only when the game is in Registration mode";
        public const string AlreadyJoinedErrorMessage = "This player is already joined the gather.";
        public const string PlayerIsNotPartOfTheGameErrorMessage = "This player is not participating on this game.";
        public const string NotInRegistrationOrNoFreeSlotErrorMessage = "The game is not in Registration mode or there is no free slot available.";
        public const string PlayerIsBannedErrorMessage = "Banned players cannot participate games!";
        public const string InvalidRequestParametersErrorMessage = "Your request parameters are invalid!";
    }
}