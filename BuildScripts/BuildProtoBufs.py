import subprocess

sourceDirectory = "..\\Common\\Networking\\protobuf\\"
destDirectory = "..\\Common\\Networking\\Messages\\"

res = subprocess.run(["protoc.exe", "-I=" + sourceDirectory, "--csharp_out=" + destDirectory, sourceDirectory + "Pong.proto"])

print(res.stdout)
print(res.stderr)
