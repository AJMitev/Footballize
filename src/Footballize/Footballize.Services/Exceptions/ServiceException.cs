namespace Footballize.Services.Exceptions
{
    using System;

    public class ServiceException : Exception
    {
        public const string RequiredNumberOfPlayersNotReached = "Cannot start this game because the required number of players is not reached yet!";
        public const string ThisGameIsAlreadyStarted = "Cannot start this game because its already started!";
        public const string ThisGameIsNotStartedYet = "Cannot complete game witch is not started!";
        public const string KickPlayerOnlyInRegistrationMode = "You can manage players only when the game is in Registration mode";
        public const string AlreadyJoined = "This player is already joined the gather.";
        public const string PlayerIsNotPartOfTheGame = "This player is not participating on this game.";
        public const string NotInRegistrationOrNoFreeSlot = "The game is not in Registration mode or there is no free slot available.";
        public const string InvalidRequestParameters = "Your request parameters are invalid!";
        public const string PlayerIsBannerd = "Banned players cannot participate games!";

        public ServiceException(string message) : base(message)
        {
        }
    }
}