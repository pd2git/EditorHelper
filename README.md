# Editor Helper

Language：English, [简体中文](/README_CN.md)

This is an editor code writing assistant that provides some general features to make it easier to write editor code.
<br>See the folders **_"[Example](/Samples/Example/)"_** and **_"[Documentation](/Documentation)"_** for details.

## Version[1.1.0]

## Function
- Save properties to EditorPres using attributes.
- Use templates to show the step base feature page.
- Use templates to show base feature pages that guide users through a specific flow to use or configure specific features.
- Use templates to show feature pages that guide the user through the overall features or show the brand image when the editor starts, such as the welcome page category guide.

## Usage

### Save Attribute

1. Mark the feature on the attribute to be saved.
2. When necessary, call the corresponding method to save the modified properties.
3. When the property data needs to be read, the corresponding method is called to read it at any time.

### Process template

1. Inherit the corresponding basic class, and can directly have its basic common functions.
2. Call or extend the corresponding method to achieve custom functions.
3. The step base page has a property saving mechanism to save the values in the step.
4. The Process foundation page provides the basic functions of the page header and page tail as well as the progress control area.

## Dependence
None.