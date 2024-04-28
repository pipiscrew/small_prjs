# Devart Entity Developer - Build to Azure DevOps Pipelines

ref - [https://www.pipiscrew.com/threads/devart-entity-developer-build-to-azure-devops-pipelines.115170/](https://www.pipiscrew.com/threads/devart-entity-developer-build-to-azure-devops-pipelines.115170/)  

On **Devart.Data.Entity.targets** the  
`$(MSBuildThisFileDirectory)` is a MSBuild property that **represents** the directory of the current .targets file.  

add the **Devart.Data.Entity.Build.Tasks.dll** near the **libs\Devart.Data.Entity.targets** file.