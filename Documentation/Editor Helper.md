# About Editor Helper

This is an editor code writing assistant that provides some general features to make it easier to write editor code.

This package includes examples about the features above. For more information, see the folder "Samples":

- There is a folder named **_"Editor"_** in the path "Samples/Example/Scripts".
- It is a example about the usage for each features.

# Installing Editor Helper

To install this package, follow the any one method below:

- Downloaded the release unity package file to local and import it to the project. See [Importing local asset packages](https://docs.unity3d.com/Manual/AssetPackagesImport.html).
- [Git URL](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html)
- [Local Folder](https://docs.unity3d.com/Manual/upm-ui-local.html)

For more information, see [Package Manager Window](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html)
 
# Using Editor Helper

### Save Attribute

1. Mark the feature on the attribute to be saved.
2. When necessary, call the corresponding method to save the modified properties.
3. When the property data needs to be read, the corresponding method is called to read it at any time.

### Process template

1. Inherit the corresponding basic class, and can directly have its basic common functions.
2. Call or extend the corresponding method to achieve custom functions.
3. The step base page has a property saving mechanism to save the values in the step.
4. The Process foundation page provides the basic functions of the page header and page tail as well as the progress control area.

# Technical details

## Requirements

This version of Editor Helper is compatible with the following versions of the Unity Editor:

* 2019.4 and later (recommended)

## Known limitations

None.

## Package contents

The following table indicates the features of each folder or file in the location below:

|Location|Description|
|---|---|
|`Scripts\Editor\`|Contains the core codes of the framework.|
|`Documentation`|Contains the documents of API by English and Chinese.|
|`Samples\Example\Scripts\Editor\FlowPage&Save\`|Contains a example of the usage about the flow page and persistence saving.|
|`Samples\Example\Scripts\Editor\StartupPage\`|Contains a example of the usage about the startup page.|

## Document revision history

|Date|Reason|
|---|---|
|2023-07-25|Unedited. Published to package.|
|2023-07-24|Document created. Matches package version 1.0.0|
