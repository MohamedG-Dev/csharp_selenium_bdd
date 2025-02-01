Feature: PageObjectModel Example

A demo of Page object model in the specflow - search testers talk in you tube

@TestersTalk
Scenario: page object model example - search text in you tube
	Given Navigate to the Youtube website
	When Search for the 'TestersTalk' in youtube
	And Navigate to the Channerl Page
	Then Verify the title of the page <pageTitle>
	Examples: 
	| pageTitle              |
	| Testers Talk - YouTube |
