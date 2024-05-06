Feature: CreateCoin

Create a coin

@CreateCoin
Scenario: Create a coin return a guid
	Given A new coin with the following properties
		| Property | Value     |
		| Symbol   | BTC	   |
		| Name     | Bitcoin   |
		| Price	   | 57496.36  |
	When I create the coin
	Then I get a guid

Scenario: Create a coin with empty symbol
	Given A new coin with the following properties
		| Property | Value     |
		| Symbol   |     	   |
		| Name     | Bitcoin   |
		| Price	   | 57496.36  |
	When I create the coin
	Then I have an error message "Coin symbol cannot be empty"

Scenario: Create a coin with empty name
	Given A new coin with the following properties
		| Property | Value     |
		| Symbol   | BTC	   |
		| Name     |		   |
		| Price	   | 57496.36  |
	When I create the coin
	Then I have an error message "Coin name cannot be empty"

Scenario: Create a coin with negative price
	Given A new coin with the following properties
		| Property | Value     |
		| Symbol   | BTC	   |
		| Name     | Bitcoin   |
		| Price	   | -2555     |
	When I create the coin
	Then I have an error message "Coin price cannot be negative"

Scenario: Coin already exists
	Given An existing coin with the following properties
		| Property | Value     |
		| Symbol   | BTC	   |
		| Name     | Bitcoin   |
		| Price	   | 57496.37  |
	When I create a new coin with the same symbol
	Then I have an error message "Coin BTC already exists"