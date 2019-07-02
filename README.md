# Project - Footballize

## Type - Football Gathering System

## Description

Footballize is a simple Football Gathering System project which allows its users to easily organize a football event in their city or take a part of one that has already been created.

Guest User can register, login to his account and view created events. Regular User can view all events, also he can create, edit and delete his own events. Also can save other users to his "Playpals" list and invite them to participate when createing a new match. The project also supports Administration. Administrator have all rights that a Regular User has. He can delete and edit all events in the system. He can also take away Users permission to publish a new event, if he break a law. Administrators can Add and Remove system information like add new city or playfield.

## Entities

### User
  - Id (string)
  - Username (string)
  - Password (string)
  - Email (string)
  - Full Name (string)
  - Phone Number (string)
  - IsBannded (bool)
  - Playpals (list of User)
  - PlayedGames (list of Events)
### Country
  - Id (string)
  - Name (string)
  - Provinces (list of Province)
### Province
  - Id (string)
  - Name (string)
  - Country (ref Country)
  - Municipalities (list of Municipality)
### Municipality
  - Id (string)
  - Name (string)
  - Province (ref Province)
  - Towns (list of Town)
### Town
  - Id (string)
  - Name (string)
  - Manicipality (ref Manicipality)
  - Addresses (list of Address)
### Address
  - Id (string)
  - Street (string)
  - Number (int)
  - Town (ref Town)
### Pitch
  - Id (string)
  - Name (string)
  - Address (ref Address)
### Event
  - Id (string)
  - Name (string)
  - Description (string)
  - Pitch (ref Pitch)
  - StartingAt (DateTime)
  - Duration (TimeSpan)
  - Team Format (Enumeration - SixPlusOne, FivePlusOne, FourPlusOne, ElevenPlayers)
  - Team Home (list of User)
  - Team Away (list of User)