# Project - Footballize

## Type - Football Gathering System

## Description

Footballize is a simple Football Gathering System project which allows its users to easily organize a football event in their city or take part in an event that has already been created. It is only necessary to register and fill out several details.

Guest Users can register, login to their accounts and view created events.
Regular Users can view and create events. Also they can save other users to their "Playpals" list and invite them to participate when createing a new event.

The project also supports Administration. Administrators have all rights a Regular User has. They can also take away Users permission to publish new event if he breaks the laws. Administrators can Add and Remove system information like add new city or playfield.

## Entities

### User
  - Id (string)
  - Username (string)
  - Password (string)
  - Email (string)
  - Full Name (string)
  - Phone Number (string)
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
  - Pitch (ref Pitch)
  - StartingAt (DateTime)
  - Duration (TimeSpan)
  - Team Format (Enumeration - SixPlusOne, FivePlusOne, FourPlusOne, ElevenPlayers)
  - Team Home (list of User)
  - Team Away (list of User)