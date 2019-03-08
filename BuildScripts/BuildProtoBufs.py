import subprocess

import os
dir_path = os.path.dirname(os.path.realpath(__file__))

sourceDirectory = "..\\Common\\Networking\\protobuf\\"
sourceProto = "..\\Common\\Networking\\protobuf\\Pong.proto"
destDirectory = "..\\Common\\Networking\\Messages\\"

res = subprocess.run(["protoc.exe", "-I=" + sourceDirectory, "--csharp_out=" + destDirectory, sourceProto])
print(res.returncode)
print(res.stdout)
print(res.stderr)
