import json
import os

with open("task-definition.json") as f:
    task_def = json.load(f)

image = f"{os.environ['ECR_REPO_URI']}:{os.environ['IMAGE_TAG']}"
container_name = os.environ["CONTAINER_NAME"]

for container in task_def["containerDefinitions"]:
    if container["name"] == container_name:
        container["image"] = image
        env = container.setdefault("environment", [])

        def set_env(name, value):
            for item in env:
                if item["name"] == name:
                    item["value"] = value
                    return
            env.append({"name": name, "value": value})

        set_env("BUILD_NUMBER", os.environ["BUILD_NUMBER"])
        set_env("IMAGE_TAG", os.environ["IMAGE_TAG"])

for field in [
    "taskDefinitionArn",
    "revision",
    "status",
    "requiresAttributes",
    "compatibilities",
    "registeredAt",
    "registeredBy"
]:
    task_def.pop(field, None)

with open("new-task-definition.json", "w") as f:
    json.dump(task_def, f)