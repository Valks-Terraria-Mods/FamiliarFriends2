using System;

namespace ValksTerrariaLib;

public interface IClosable
{
    event Action CloseRequested;
}
