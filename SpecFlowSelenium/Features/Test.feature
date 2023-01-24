Feature: Specflow

@test
Scenario: Implement an automated test covering the following flow. Using C#, selenium and SpecFlow.
	
	Given I Login to the site with credentials gainchanger / justdoit
	When I navigate to the following url 
	And Access the first blog in the list of blog posts present in the resources page
	Then I can extract the following fields from the blog page. (H1, Meta title, Meta description, H2 elements, Paragraph elements)
	And Export the fields in a file in json format