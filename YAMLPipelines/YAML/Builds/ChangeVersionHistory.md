[[_TOC_]]

# Build YAML Templates

Shared and used by teams as a repeatable process during pipeline execution

Usage: [Azure DevOps Using Repositories for Shared Templates](https://docs.microsoft.com/en-us/azure/devops/pipelines/process/templates?view=azure-devops#using-other-repositories)


##1. LOCATION: DOCKER FOLDER

**Docker-1-ContainerRegistry** - template for building and publishing docker containers to the untrusted Azure Container Registry
- V1.0 = Initial Commit.

**Docker-2-TrustedRegistry** - template for building and publishing docker containers to the trusted Azure Container Registry
- V1.0 = Initial Commit.

**DockerBuildAndPublishUntrusted.yml** - template for building and publishing docker containers to untrusted Azure Container Registry
- V1.0 = Initial Commit.
- V1.1 = Updating Param Node names

**DockerBuildAndPublish.yml** - template for building and publishing docker containers on Azure Container Registry (Deprecating)
- V1.0 = Initial Commit.
- V2.0 = Added new optional parameters dockerimagetag, dockerbuildargs
- V2.1 = Added --no-cache on build commands
- V3.1 = Changing Docker Steps to use PowerShell so escape characters " always work on both linux and windows

**Docker/ContinousIntegration.yml** - template for running centralized continous integration tasks (Deprecating)
- V1.0 = Initial Commit. Added new markup adoption file (ContinousIntegration.md)
- V1.1 = Movied Download Prep Directly in the Build and Publish shared template

##2. LOCATION: NETCORE FOLDER

**NetCore/ContinousIntegration.yml** - template for running centralized continous integration tasks
- V1.0 = Initial Commit.
- V2.0 = Adding initial CI jobs
- ***V3.0*** = MAJOR RELEASE:
	- Add new Shared Templates for .Net Core 
	- Refactored build templates according to platform. 
	- Created new ContinousIntegration.md file for .Net Core
- V3.1 = Added Additional SonarCloud Exclusion Parameters
- V3.2 = Added PathToNugetConfig parameter
- V3.3 = Removed SonarCloud Exclusion Paramters, Added condition to run scan only if both sonar props have values, Added AgentPoolName param for logging 
- ***V4.0*** = MAJOR RELEASE:
	- Switched from using SonarCloudScanNetCore.yml to **SonarQubeScanNetCore.yml**
- V4.1 = Removed Coveage Params
- ***V5.0*** = MAJOR RELEASE:
	- Removing standalone BuildAndPublish and we're now just using 1 job: Removing standalone QualityAndSecurityBuild
- V5.1 = Consolidate DeploymentResources job, added new nodes: StageContainsPrepForBuildJob, CreateDeploymentArtifact, DeploymentFolderLocation
- V5.2 = Addeded display name on download steps
- V5.3 = Check for .Net Core SDK parameter install for app ingestion steps

**NetCore/DotNetCoreBuildAndPublish.yml** - template for building and publishing .Net Core applications to Azure Devops 
- V1.0 = Initial Commit.
- V2.0 = Added support for passing in custom .Net Core SDK version: NetCoreSDKVersion (e.g: NetCoreSDKVersion: '2.2.300')
- V2.1 = Added PathToNugetConfig parameter
- V2.2 = Added AgentPoolName param for logging 
- V2.2 = Added SelfContained param true/false so we can publish self-contained bits to Azure App Service environments using preview versions of .Net Core
- V3.0 = Removed Flag for publish artifacts. We're always going to publush artifacts
- V4.0 = Switch from Azure Devops task: 
		Original: DotNetCoreCLI@2/publish to two steps
		Modified: script: dotnet publish + task: ArchiveFiles@2

**NetCore/DotNetCoreQualitySteps.yml** - template for executing .Net Core Tests
- V1.0 = Initial Commit.
- V2.0 = Adding Runsettings file and TestCategories parameters.
- V3.0 = Added CheckCoverage parameter and disabled Code Coverage Task if CheckCodeCoverage parameter is false or CoverageThreshold is set to 0.
- V4.0 = Added support for passing in custom .Net Core SDK version: NetCoreSDKVersion (e.g: NetCoreSDKVersion: '2.2.300')
- V4.1 = Added PathToNugetConfig parameter
- V4.2 = BUG FIX: Removed sonar exclusions when additionalFileExclusions is ''
- V4.3 = Added More Logging
- V4.4 = Removed Coverage Check since handled in SonarQube

##3. LOCATION: NETFX FOLDER

**NetFx/ContinuousIntegration.yml** - template for running centralized continous integration tasks
- V1.0 = Initial Commit

**NetFx/NetFxPublish.yml** - template for publishing projects
- V1.0 = Initial Commit

**NetFx/NetFxRestoreAndBuild.yml** - template for restoring and building projects
- V1.0 = Initial Commit
- V1.1 = Fix bug with with nuget restore.  When nuget config file is specified, then identify solution that should be used for the nuget package restore

**NetFx/NetFxTest.yml** - template for testing projects 
- V1.0 = Initial Commit

##4. LOCATION: NODE FOLDER

**Node/ContinousIntegration.yml** - template for running centralized continous integration tasks
- ***V4.0*** = MAJOR RELEASE:
	- Switched from using SonarCloudScanNode.yml to **SonarQubeScanNode.yml**
- V4.1 = Added SQ Props in order to use shared SQ Prepare task, added DeploymentFolderLocation node to allow override, added comments

##5. LOCATION: PYTHON FOLDER

**Python/ContinousIntegration.yml** - template for testing projects 
- V1.0 = Initial Commit

##6. LOCATION: REACT FOLDER

**ReactBuildAndPublish.yml** - template for building and publishing React applications to Azure DevOps. Uses NPM Install and Build
- V1.0 = Initial Commit.

**ReactQualitySteps.yml** - template for executing React Tests
- V1.0 = Initial Commit.

##7. LOCATION: SECURITY FOLDER

**HpFortifyStaticScan.yml** - template for running HP Security Scans
- V1.0 = Initial Commit. 
- V1.1 = Updated build task to version 3. Added RestoreAndBuild parameter to allow the project to be build prior to running triggering the scan. Added the ContinueOnError parameter to continue the build even if security task fails.

**Security/SonarQubeAnalyzeAndPublish.yml** - template containing SQ Analyze, Publish, and BuildBreaker tasks
- V1.0 = Initial Commit

**Security/SonarQubePrepareCLI.yml** - template containing SQ Prepare task with CLI. 
- V1.0 = Initial Commit

**Security/SonarQubePrepareMsBuild.yml** - template containing SQ Prepare task with MsBuild apps. 
- V1.0 = Initial Commit

**Security/SonarQubeScanNetCore.yml** - template for running SonarQube scan for .net core apps
- V1.0 = Enabled SQ tasks after infrastructure setup
- V1.1 = Added reference to shared SQ tasks

**Security/SonarQubeScanNode.yml** - template for running SonarQube scan for node apps
- V1.0 = Enabled SQ tasks after infrastructure setup

##8. LOCATION: COMMON FOLDER

**AppIngestion.yml.yml** - template for publishing deployment resources
- V1.0 = Initial Commit. 
	- Moved logic from NetCore CI template
	- Added AppIngestion Steps for Application Tracing

##DELETED FILES

**CI_StageDocker.yml** - template that orchestrates the building and publishing docker containers to the trusted and untrusted Azure Container Registries
- V1.0 = Initial Commit.
- V1.1 = Updating Param Node names
- DEPRECATED - Runtime variables did not resolve correctly at stage level

**SonarCloudScanNetCore.yml** - template for running HP Security Scans
- V1.0 = Initial Commit. 
	- Removed sonar.branch.name 
	- Added *.tests exclusions
- V1.1 = Added Additional SonarCloud Exclusion Parameters
- V1.2 = Added PathToNugetConfig parameter
- V2.0 = Removed SonarCloud Exclusions, updated logging, removed additionalFileExclusionFilePath conditionals, added condition to run scan only if both sonar props have values, added AgentPoolName param for logging 
- ***V2.0*** = DELETED

**SonarCloudScanNode.yml** - template for running HP Security Scans
- V1.0 = Initial Commit. 
	- Removed sonar.branch.name 
	- Added scanned sources parameter (Scannedsources)
- ***V2.0*** = DELETED