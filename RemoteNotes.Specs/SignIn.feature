Feature: SignIn
	In order to enter the system
	As a registered user
	I want to sign in with email and password

Scenario: Successful sign in
	Given I have connected to API by url https://remote-notes.herokuapp.com/
	When I sign in with email bb@aa.aa and password aa123456
	Then the result should be a User object with same email

Scenario: Unsuccessful sign in
	Given I have connected to API by url https://remote-notes.herokuapp.com/
	When I sign in with email aa@aa32.aa and password aa123456
	Then exception is thrown
    
Scenario: Authenticated Sign out
	Given I have connected to API by url https://remote-notes.herokuapp.com/
    Given I am signed in with email bb@aa.aa and password aa123456
    When I sign out
    Then exception is not thrown

Scenario: Unauthenticated sign out
	Given I have connected to API by url https://remote-notes.herokuapp.com/
    When I sign out
    Then exception is thrown

Scenario: Profile update
    Given I have connected to API by url https://remote-notes.herokuapp.com/
    Given I am signed in with email bb@aa.aa and password aa123456
    When I update my account with random name and base64 image
    Then returned user has same name and image