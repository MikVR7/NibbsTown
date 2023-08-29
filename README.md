# NibbsTown

This software is under development and is currently in the prototyping and experimentation phase.

NibbsTown is an app that makes school group excursions more convenient and fun. It runs on Android and iOS devices. Instead of running around with slips of paper students use NibbsTown to discover and learn about a city/town/area. 

The core of the app consists of **tours**. A tour is a sequence of stations that combined represent a certain thematic. A **station** is supposed to be in one geographical spot and consists of one or more tasks. A **task** can be an exercise, an information, a game or an other short activity.

## Project type
The project is built in Unity and the database runs on Firebase. There are also a few Assetstore packages that I bought and that are necessary for the project. Please don't copy them and don't use them in other projects.

## Database
The database runs on Google's Firebase. The API key of the database is also committed, so everyone who wants to play around with NibbsTown can do so until my free Firebase resources are used up. In case the free resources get used up I'll have to change the API key.

## AR
It is planned to use AR (AR Foundation by Unity) a lot in NibbsTown. However at the current point there are no tasks including AR yet. This is comming soon, since I have a finished AR-game from an older NibbsTown demo. It's a game where you have to chase objects that fly around you (dragons).

## NibbsTown main system

Under Assets/MikVR/NibbsTown is the main NibbsTown project. It consists of
- Loading screen
- Login screen (very rudimentary - just enter your name)
- Rally selection (currently there are 3 rallies - "Zauberwald" is the main development candidate)
- Map (the map system is built upon the "Online Maps v3" package)
- Info screen (can contain Text and Pictures loaded from the database

## EventSystem
Based on the Unity EventSystem I build my own event system since i needed more functionality.

## Tasks

### Cloze
This is a classic cloze "game" where the students can show what they've learned. Input can be entered over a classical input field or a dropdown list. The GUI immediately shows the user if the input was correct. The cloze will be generated dynamically on runtime considering the input text.

### PicturePuzzle
A classical puzzle game

### Constructor
2D physically based game - under development, but can be tested already - haven't figured out the rules yet.

## Map
Map shows the position of the player and the stations of the rally. The active stations are animated. In editor mode there is a gui that lets you move around the map. If you build to mobile you have to be at the required gps position to complete a station. Map system is based on Online Maps v3.

## Create your own Tour
If you want to create your own tour, or see how it's built look into RallyCreator.cs
