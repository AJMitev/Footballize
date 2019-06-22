# Project - Footbalize

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
### Country
  - Id (string)
  - Name (string)
  - Cities (list of City)
### City
  - Id (string)
  - Name (string)
  - Playfields (list of Playfield)
### Playfiled
  - Id (string)
  - Name (string)
  - Country (Entity)
  - City (Entity)
### Event
  - Id (string)
  - Name (string)
  - Country (Entity)
  - City (Entity)
  - StartingAt (DateTime)
  - Duration (TimeSpan)
  - Team Home (list of User)
  - Team Away (list of User)