# Azure Portal manual setup for CardLearning

This guide is for the learning path where you create the Azure resources manually in Azure Portal and do not use Bicep.

Goal:

1. Create the Azure resources through the Azure Portal UI.
2. Configure GitHub Actions authentication with OIDC.
3. Prepare the existing workflows so that `Deploy Dev` can push an image to Azure Container Registry and update Azure Container Apps.

The workflows in this repository assume these names by default:

- Resource group: `rg-cardlearning-dev-plc`
- Container registry: your own unique name, saved as `AZURE_ACR_NAME`
- Log Analytics workspace: `log-cardlearning-dev`
- Container Apps environment: `cae-cardlearning-dev`
- Container app: `ca-cardlearning-api-dev`
- GitHub environment: `dev`

Useful references:

- [Register an application in Microsoft Entra ID](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-register-app)
- [Use the Azure Login action with OpenID Connect](https://learn.microsoft.com/en-us/azure/developer/github/connect-from-azure-openid-connect)
- [Assign Azure roles using the Azure portal](https://learn.microsoft.com/en-us/azure/role-based-access-control/role-assignments-portal)
- [Create an Azure container registry by using the Azure portal](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-get-started-portal)
- [Deploy an existing container image in the Azure portal](https://learn.microsoft.com/en-us/azure/container-apps/get-started-existing-container-image-portal)
- [Azure Container Apps image pull with managed identity](https://learn.microsoft.com/en-us/azure/container-apps/managed-identity-image-pull)

## Before you start

You need:

1. An active Azure subscription.
2. Access to Azure Portal.
3. Enough permissions to create resources and role assignments in that subscription.

For role assignments, the safest assumption is that your account needs one of these roles on the subscription or resource group scope:

- `Owner`
- `User Access Administrator`
- or another role that includes `Microsoft.Authorization/roleAssignments/write`

## 1. Check the subscription and region

1. Open [Azure Portal](https://portal.azure.com/).
2. Search for `Subscriptions`.
3. Open your subscription.
4. Copy the **Subscription ID**.
5. Confirm which region you want to use. Start with `Poland Central`.

If `Poland Central` is unavailable for one of the resources in your subscription, use `West Europe` for all dev resources instead.

Save:

- `Subscription ID` -> `AZURE_SUBSCRIPTION_ID`

## 2. Create the resource group

1. Search for `Resource groups`.
2. Click **Create**.
3. Select your subscription.
4. Resource group name: `rg-cardlearning-dev-plc`
5. Region: `Poland Central`
6. Click **Review + create**.
7. Click **Create**.

This resource group will contain the application resources.

## 3. Create the GitHub identity in Microsoft Entra ID

This is the identity GitHub Actions will use to log into Azure.

1. Search for `Microsoft Entra ID`.
2. Open **App registrations**.
3. Click **New registration**.
4. Set:
   - Name: `id-github-cardlearning-dev`
   - Supported account types: `Accounts in this organizational directory only`
   - Redirect URI: leave empty
5. Click **Register**.

From the Overview page copy:

- **Application (client) ID** -> `AZURE_CLIENT_ID`
- **Directory (tenant) ID** -> `AZURE_TENANT_ID`

## 4. Add the federated credential for GitHub Actions

1. Open the app registration `id-github-cardlearning-dev`.
2. Open **Certificates & secrets**.
3. Open **Federated credentials**.
4. Click **Add credential**.
5. Choose **GitHub Actions deploying Azure resources**.
6. Fill in:
   - Organization: `CardLearning`
   - Repository: `back-end`
   - Entity type: `Environment`
   - Environment name: `dev`
   - Name: `github-cardlearning-dev`
7. Save.

This allows GitHub Actions to authenticate with Azure using OIDC and no client secret.

## 5. Create Azure Container Registry

1. Search for `Container registries`.
2. Click **Create**.
3. Fill in:
   - Subscription: your subscription
   - Resource group: `rg-cardlearning-dev-plc`
   - Registry name: choose a globally unique name, for example `acrcardlearningdev12345`
   - Location: `Poland Central`
   - Domain name label scope: `Tenant Reuse` for the safer default, or keep `Unsecure` if you already created the registry that way in this learning exercise
   - Pricing plan: `Basic`
   - Role assignment permissions mode: `RBAC Registry Permissions`
4. Leave the other settings as default unless your organization requires something different.
5. Click **Review + create**.
6. Click **Create**.

After creation, open the registry and copy:

- Registry name -> `AZURE_ACR_NAME`
- Login server, for example `acrcardlearningdev12345.azurecr.io`

Important notes:

- For the workflows in this repository, use `RBAC Registry Permissions`. This matches the `AcrPush` and `AcrPull` roles used later in the guide.
- If you choose `Tenant Reuse`, Azure usually adds a hash to the login server, for example `myregistry-abc123.azurecr.io`.
- If your login server is plain, for example `cardlearningbackdev.azurecr.io`, the registry was created with `Unsecure` domain name label scope. That is acceptable for this dev learning setup.

## 6. Give GitHub Actions permission to push images into ACR

1. Open the container registry you just created.
2. Open **Access control (IAM)**.
3. Click **Add** > **Add role assignment**.
4. Choose role `AcrPush`.
5. Click **Next**.
6. For member type, choose the Entra application or service principal path available in your portal.
7. Select `id-github-cardlearning-dev`.
8. Finish the assignment.

Why this is needed:

- the workflow logs into Azure as the GitHub identity;
- that identity needs permission to push Docker images into ACR.

## 7. Create Log Analytics workspace

1. Search for `Log Analytics workspaces`.
2. Click **Create**.
3. Fill in:
   - Subscription: your subscription
   - Resource group: `rg-cardlearning-dev-plc`
   - Name: `log-cardlearning-dev`
   - Region: `Poland Central`
4. Click **Review + create**.
5. Click **Create**.

Container Apps uses this workspace for logs.

## 8. Create the Container Apps environment

1. Search for `Container Apps Environments`.
2. Click **Create**.
3. Fill in:
   - Subscription: your subscription
   - Resource group: `rg-cardlearning-dev-plc`
   - Name: `cae-cardlearning-dev`
   - Region: `Poland Central`
4. In the logging section, attach the existing Log Analytics workspace `log-cardlearning-dev`.
5. Leave networking public/default for now.
6. Click **Review + create**.
7. Click **Create**.

This environment is the shared runtime boundary for your container apps.

## 9. Create the Container App

Create the app first with a public sample image. Later GitHub Actions will replace the image with your own image from ACR.

1. Search for `Container Apps`.
2. Click **Create** > **Container App**.
3. Basics:
   - Subscription: your subscription
   - Resource group: `rg-cardlearning-dev-plc`
   - Container app name: `ca-cardlearning-api-dev`
   - Region: `Poland Central`
   - Container Apps environment: `cae-cardlearning-dev`
4. Container tab:
   - Image source: public registry / Docker Hub / quickstart image path available in your portal
   - Image: `mcr.microsoft.com/dotnet/samples:aspnetapp`
5. Ingress tab:
   - Ingress: enabled
   - Ingress traffic: `Accepting traffic from anywhere`
   - Type: `HTTP`
   - Target port: `8080`
6. Scale tab:
   - Minimum replicas: `0`
   - Maximum replicas: `1`
7. Review and create the resource.

When the deployment finishes, open the resource and confirm it has an external URL.

## 10. Enable a system-assigned identity on the Container App

The Container App needs its own identity so it can pull images from ACR without registry passwords.

1. Open `ca-cardlearning-api-dev`.
2. Open **Identity**.
3. In **System assigned**, switch **Status** to `On`.
4. Save.

After saving, Azure creates an identity that belongs to this Container App.

## 11. Give the Container App permission to pull from ACR

1. Open your container registry.
2. Open **Access control (IAM)**.
3. Click **Add** > **Add role assignment**.
4. Choose role `AcrPull`.
5. Click **Next**.
6. Select the managed identity for `ca-cardlearning-api-dev`.
7. Finish the assignment.

This allows the running app to pull the image that GitHub pushes to ACR.

## 12. Give GitHub Actions permission to update the Container App

1. Open the resource group `rg-cardlearning-dev-plc`.
2. Open **Access control (IAM)**.
3. Click **Add** > **Add role assignment**.
4. Choose role `Container Apps Contributor`.
5. Click **Next**.
6. Select the app registration `id-github-cardlearning-dev`.
7. Finish the assignment.

This allows the deploy workflow to update the Container App after pushing a new image.

## 13. Add the GitHub environment values

Open GitHub for repository `CardLearning/back-end`.

1. Go to **Settings** > **Environments**.
2. Create environment `dev`.
3. Add these environment secrets or variables:

| Name | Value |
| --- | --- |
| `AZURE_CLIENT_ID` | App registration client ID |
| `AZURE_TENANT_ID` | Tenant ID |
| `AZURE_SUBSCRIPTION_ID` | Subscription ID |
| `AZURE_RESOURCE_GROUP` | `rg-cardlearning-dev-plc` |
| `AZURE_CONTAINER_APP_NAME` | `ca-cardlearning-api-dev` |
| `AZURE_ACR_NAME` | the registry name you created |

You can store them as environment secrets or variables. Secrets are the safer default.

## 14. Optional portal checks before the first merge

Before using GitHub Actions, confirm this checklist in Azure Portal:

1. `rg-cardlearning-dev-plc` exists.
2. The container registry exists and opens.
3. The Log Analytics workspace exists.
4. The Container Apps environment exists.
5. The Container App exists and has external ingress.
6. The Container App has **System assigned identity = On**.
7. The Container App identity has `AcrPull` on the registry.
8. The GitHub app registration has `AcrPush` on the registry.
9. The GitHub app registration has `Container Apps Contributor` on the resource group.

## 15. What happens during the first deployment

When you merge into `main`, the `Deploy Dev` workflow will:

1. Build and test the .NET project.
2. Log into Azure with OIDC.
3. Push a Docker image to Azure Container Registry.
4. Configure the Container App to use that registry with its system-assigned identity.
5. Update the Container App to the image tagged with the commit SHA.
6. Verify the active revision and call `/healthz`.

## Common beginner mistakes

These are the most common setup mistakes:

1. Using the wrong ID in GitHub:
   - GitHub needs `Application (client) ID`, not Object ID.
2. Assigning no roles to the GitHub identity:
   - without `AcrPush` and `Container Apps Contributor`, deploy will fail.
3. Forgetting to enable the Container App system-assigned identity:
   - then the app cannot pull from ACR through managed identity.
4. Creating the app with the wrong target port:
   - your API runs on `8080`, not `80`.
5. Using a different resource name from what the workflow expects:
   - then GitHub variables must match your actual names.
6. Choosing the wrong ACR permission mode:
   - if you select `RBAC Registry + ABAC Repository Permissions`, the old `AcrPush` and `AcrPull` roles from this guide no longer match the setup cleanly.

## What you are intentionally not doing in this learning path

In this manual Portal path, you are not using:

- Bicep
- Terraform
- private networking
- Key Vault
- production approval gates
- multiple environments

That is fine for the first pass. The point here is to understand the Azure building blocks first.
