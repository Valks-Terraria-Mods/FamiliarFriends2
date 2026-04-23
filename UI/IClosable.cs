using System;

namespace FamiliarFriends;

public interface IClosable
{
    event Action CloseRequested;
}
