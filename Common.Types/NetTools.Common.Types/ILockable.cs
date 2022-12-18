namespace NetTools.Common.Types;

public abstract class Lockable
{
    public bool IsLocked { get; private set; }

    public void Lock()
    {
        IsLocked = true;
    }

    public void Unlock()
    {
        IsLocked = false;
    }

    public void ToggleLock()
    {
        IsLocked = !IsLocked;
    }

    public void LockIfTrue(bool condition)
    {
        if (condition)
            Lock();
    }

    public void UnlockIfTrue(bool condition)
    {
        if (condition)
            Unlock();
    }

    public void ToggleLockIfTrue(bool condition)
    {
        if (condition)
            ToggleLock();
    }

    public void LockIfTrue(Func<bool> condition)
    {
        if (condition())
            Lock();
    }

    public void UnlockIfTrue(Func<bool> condition)
    {
        if (condition())
            Unlock();
    }

    public void ToggleLockIfTrue(Func<bool> condition)
    {
        if (condition())
            ToggleLock();
    }

    public void Add<T>(Func<T> addFunc)
    {
        if (IsLocked)
        {
            throw new InvalidOperationException("Cannot add to a locked object.");
        }

        addFunc();
    }

    public void Add<T>(Func<T> addFunc, Action onFail)
    {
        if (IsLocked)
        {
            onFail();
            return;
        }

        addFunc();
    }

    public void Add<T>(Func<T> addFunc, Action<T> onAdd)
    {
        if (IsLocked)
        {
            throw new InvalidOperationException("Cannot add to a locked object.");
        }

        onAdd(addFunc());
    }

    public void Add<T>(Func<T> addFunc, Action<T> onAdd, Action onFail)
    {
        if (IsLocked)
        {
            onFail();
            return;
        }

        onAdd(addFunc());
    }

    public void Remove<T>(Func<T> removeFunc)
    {
        if (IsLocked)
        {
            throw new InvalidOperationException("Cannot remove from a locked object.");
        }

        removeFunc();
    }

    public void Remove<T>(Func<T> removeFunc, Action onFail)
    {
        if (IsLocked)
        {
            onFail();
            return;
        }

        removeFunc();
    }

    public void Remove<T>(Func<T> removeFunc, Action<T> onRemove)
    {
        if (IsLocked)
        {
            throw new InvalidOperationException("Cannot remove from a locked object.");
        }

        onRemove(removeFunc());
    }

    public void Remove<T>(Func<T> removeFunc, Action<T> onRemove, Action onFail)
    {
        if (IsLocked)
        {
            onFail();
            return;
        }

        onRemove(removeFunc());
    }
}
