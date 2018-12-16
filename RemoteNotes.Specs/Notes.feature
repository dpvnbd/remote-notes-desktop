Feature: Notes
	In order to work with notes
	As a authenticated user
	I want to Create, Read, Update, Delete notes

Scenario: List notes
    Given I have connected to API by url https://remote-notes.herokuapp.com/
    Given I am signed in with email bb@aa.aa and password aa123456
    When I request notes list
    Then list of notes is returned

Scenario: Create note
    Given I have connected to API by url https://remote-notes.herokuapp.com/
    Given I am signed in with email bb@aa.aa and password aa123456
    When I create note with current time in body
    Then note with the same body is returned

Scenario: Update note
    Given I have connected to API by url https://remote-notes.herokuapp.com/
    Given I am signed in with email bb@aa.aa and password aa123456
    Given I have created a note with current time in body
    When I update the created note with body "Aaaaaaaaaaa"
    Then note with the same body is returned

Scenario: Delete note
    Given I have connected to API by url https://remote-notes.herokuapp.com/
    Given I am signed in with email bb@aa.aa and password aa123456
    Given I have created a note with current time in body
    When I delete the created note
    Then exception is not thrown