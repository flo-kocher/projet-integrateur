using System;
using System.Collections;
using System.Collections.Generic;

namespace Mirror
{
<<<<<<< HEAD
    public class SyncList<T> : SyncObject, IList<T>, IReadOnlyList<T>
=======
    // Deprecated 10/02/2020
    [Obsolete("Use SyncList<string> instead")]
    public class SyncListString : SyncList<string> {}

    // Deprecated 10/02/2020
    [Obsolete("Use SyncList<float> instead")]
    public class SyncListFloat : SyncList<float> {}

    // Deprecated 10/02/2020
    [Obsolete("Use SyncList<int> instead")]
    public class SyncListInt : SyncList<int> {}

    // Deprecated 10/02/2020
    [Obsolete("Use SyncList<uint> instead")]
    public class SyncListUInt : SyncList<uint> {}

    // Deprecated 10/02/2020
    [Obsolete("Use SyncList<bool> instead")]
    public class SyncListBool : SyncList<bool> {}

    public class SyncList<T> : IList<T>, IReadOnlyList<T>, SyncObject
>>>>>>> origin/alpha_merge
    {
        public delegate void SyncListChanged(Operation op, int itemIndex, T oldItem, T newItem);

        readonly IList<T> objects;
        readonly IEqualityComparer<T> comparer;

        public int Count => objects.Count;
        public bool IsReadOnly { get; private set; }
        public event SyncListChanged Callback;

        public enum Operation : byte
        {
            OP_ADD,
            OP_CLEAR,
            OP_INSERT,
            OP_REMOVEAT,
            OP_SET
        }

        struct Change
        {
            internal Operation operation;
            internal int index;
            internal T item;
        }

<<<<<<< HEAD
        // list of changes.
        // -> insert/delete/clear is only ONE change
        // -> changing the same slot 10x caues 10 changes.
        // -> note that this grows until next sync(!)
        readonly List<Change> changes = new List<Change>();

=======
        readonly List<Change> changes = new List<Change>();
>>>>>>> origin/alpha_merge
        // how many changes we need to ignore
        // this is needed because when we initialize the list,
        // we might later receive changes that have already been applied
        // so we need to skip them
        int changesAhead;

        public SyncList() : this(EqualityComparer<T>.Default) {}

        public SyncList(IEqualityComparer<T> comparer)
        {
            this.comparer = comparer ?? EqualityComparer<T>.Default;
            objects = new List<T>();
        }

        public SyncList(IList<T> objects, IEqualityComparer<T> comparer = null)
        {
            this.comparer = comparer ?? EqualityComparer<T>.Default;
            this.objects = objects;
        }

<<<<<<< HEAD
        // throw away all the changes
        // this should be called after a successful sync
        public override void ClearChanges() => changes.Clear();

        public override void Reset()
=======
        public bool IsDirty => changes.Count > 0;

        // throw away all the changes
        // this should be called after a successful sync
        public void Flush() => changes.Clear();

        public void Reset()
>>>>>>> origin/alpha_merge
        {
            IsReadOnly = false;
            changes.Clear();
            changesAhead = 0;
            objects.Clear();
        }

        void AddOperation(Operation op, int itemIndex, T oldItem, T newItem)
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("Synclists can only be modified at the server");
            }

            Change change = new Change
            {
                operation = op,
                index = itemIndex,
                item = newItem
            };

<<<<<<< HEAD
            if (IsRecording())
            {
                changes.Add(change);
                OnDirty?.Invoke();
            }
=======
            changes.Add(change);
>>>>>>> origin/alpha_merge

            Callback?.Invoke(op, itemIndex, oldItem, newItem);
        }

<<<<<<< HEAD
        public override void OnSerializeAll(NetworkWriter writer)
        {
            // if init,  write the full list content
            writer.WriteUInt((uint)objects.Count);
=======
        public void OnSerializeAll(NetworkWriter writer)
        {
            // if init,  write the full list content
            writer.WriteUInt32((uint)objects.Count);
>>>>>>> origin/alpha_merge

            for (int i = 0; i < objects.Count; i++)
            {
                T obj = objects[i];
                writer.Write(obj);
            }

            // all changes have been applied already
            // thus the client will need to skip all the pending changes
            // or they would be applied again.
            // So we write how many changes are pending
<<<<<<< HEAD
            writer.WriteUInt((uint)changes.Count);
        }

        public override void OnSerializeDelta(NetworkWriter writer)
        {
            // write all the queued up changes
            writer.WriteUInt((uint)changes.Count);
=======
            writer.WriteUInt32((uint)changes.Count);
        }

        public void OnSerializeDelta(NetworkWriter writer)
        {
            // write all the queued up changes
            writer.WriteUInt32((uint)changes.Count);
>>>>>>> origin/alpha_merge

            for (int i = 0; i < changes.Count; i++)
            {
                Change change = changes[i];
                writer.WriteByte((byte)change.operation);

                switch (change.operation)
                {
                    case Operation.OP_ADD:
                        writer.Write(change.item);
                        break;

                    case Operation.OP_CLEAR:
                        break;

                    case Operation.OP_REMOVEAT:
<<<<<<< HEAD
                        writer.WriteUInt((uint)change.index);
=======
                        writer.WriteUInt32((uint)change.index);
>>>>>>> origin/alpha_merge
                        break;

                    case Operation.OP_INSERT:
                    case Operation.OP_SET:
<<<<<<< HEAD
                        writer.WriteUInt((uint)change.index);
=======
                        writer.WriteUInt32((uint)change.index);
>>>>>>> origin/alpha_merge
                        writer.Write(change.item);
                        break;
                }
            }
        }

<<<<<<< HEAD
        public override void OnDeserializeAll(NetworkReader reader)
=======
        public void OnDeserializeAll(NetworkReader reader)
>>>>>>> origin/alpha_merge
        {
            // This list can now only be modified by synchronization
            IsReadOnly = true;

            // if init,  write the full list content
<<<<<<< HEAD
            int count = (int)reader.ReadUInt();
=======
            int count = (int)reader.ReadUInt32();
>>>>>>> origin/alpha_merge

            objects.Clear();
            changes.Clear();

            for (int i = 0; i < count; i++)
            {
                T obj = reader.Read<T>();
                objects.Add(obj);
            }

            // We will need to skip all these changes
            // the next time the list is synchronized
            // because they have already been applied
<<<<<<< HEAD
            changesAhead = (int)reader.ReadUInt();
        }

        public override void OnDeserializeDelta(NetworkReader reader)
=======
            changesAhead = (int)reader.ReadUInt32();
        }

        public void OnDeserializeDelta(NetworkReader reader)
>>>>>>> origin/alpha_merge
        {
            // This list can now only be modified by synchronization
            IsReadOnly = true;

<<<<<<< HEAD
            int changesCount = (int)reader.ReadUInt();
=======
            int changesCount = (int)reader.ReadUInt32();
>>>>>>> origin/alpha_merge

            for (int i = 0; i < changesCount; i++)
            {
                Operation operation = (Operation)reader.ReadByte();

                // apply the operation only if it is a new change
                // that we have not applied yet
                bool apply = changesAhead == 0;
                int index = 0;
                T oldItem = default;
                T newItem = default;

                switch (operation)
                {
                    case Operation.OP_ADD:
                        newItem = reader.Read<T>();
                        if (apply)
                        {
                            index = objects.Count;
                            objects.Add(newItem);
                        }
                        break;

                    case Operation.OP_CLEAR:
                        if (apply)
                        {
                            objects.Clear();
                        }
                        break;

                    case Operation.OP_INSERT:
<<<<<<< HEAD
                        index = (int)reader.ReadUInt();
=======
                        index = (int)reader.ReadUInt32();
>>>>>>> origin/alpha_merge
                        newItem = reader.Read<T>();
                        if (apply)
                        {
                            objects.Insert(index, newItem);
                        }
                        break;

                    case Operation.OP_REMOVEAT:
<<<<<<< HEAD
                        index = (int)reader.ReadUInt();
=======
                        index = (int)reader.ReadUInt32();
>>>>>>> origin/alpha_merge
                        if (apply)
                        {
                            oldItem = objects[index];
                            objects.RemoveAt(index);
                        }
                        break;

                    case Operation.OP_SET:
<<<<<<< HEAD
                        index = (int)reader.ReadUInt();
=======
                        index = (int)reader.ReadUInt32();
>>>>>>> origin/alpha_merge
                        newItem = reader.Read<T>();
                        if (apply)
                        {
                            oldItem = objects[index];
                            objects[index] = newItem;
                        }
                        break;
                }

                if (apply)
                {
                    Callback?.Invoke(operation, index, oldItem, newItem);
                }
                // we just skipped this change
                else
                {
                    changesAhead--;
                }
            }
        }

        public void Add(T item)
        {
            objects.Add(item);
            AddOperation(Operation.OP_ADD, objects.Count - 1, default, item);
        }

        public void AddRange(IEnumerable<T> range)
        {
            foreach (T entry in range)
            {
                Add(entry);
            }
        }

        public void Clear()
        {
            objects.Clear();
            AddOperation(Operation.OP_CLEAR, 0, default, default);
        }

        public bool Contains(T item) => IndexOf(item) >= 0;

        public void CopyTo(T[] array, int index) => objects.CopyTo(array, index);

        public int IndexOf(T item)
        {
            for (int i = 0; i < objects.Count; ++i)
                if (comparer.Equals(item, objects[i]))
                    return i;
            return -1;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < objects.Count; ++i)
                if (match(objects[i]))
                    return i;
            return -1;
        }

        public T Find(Predicate<T> match)
        {
            int i = FindIndex(match);
            return (i != -1) ? objects[i] : default;
        }

        public List<T> FindAll(Predicate<T> match)
        {
            List<T> results = new List<T>();
            for (int i = 0; i < objects.Count; ++i)
                if (match(objects[i]))
                    results.Add(objects[i]);
            return results;
        }

        public void Insert(int index, T item)
        {
            objects.Insert(index, item);
            AddOperation(Operation.OP_INSERT, index, default, item);
        }

        public void InsertRange(int index, IEnumerable<T> range)
        {
            foreach (T entry in range)
            {
                Insert(index, entry);
                index++;
            }
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            bool result = index >= 0;
            if (result)
            {
                RemoveAt(index);
            }
            return result;
        }

        public void RemoveAt(int index)
        {
            T oldItem = objects[index];
            objects.RemoveAt(index);
            AddOperation(Operation.OP_REMOVEAT, index, oldItem, default);
        }

        public int RemoveAll(Predicate<T> match)
        {
            List<T> toRemove = new List<T>();
            for (int i = 0; i < objects.Count; ++i)
                if (match(objects[i]))
                    toRemove.Add(objects[i]);

            foreach (T entry in toRemove)
            {
                Remove(entry);
            }

            return toRemove.Count;
        }

        public T this[int i]
        {
            get => objects[i];
            set
            {
                if (!comparer.Equals(objects[i], value))
                {
                    T oldItem = objects[i];
                    objects[i] = value;
                    AddOperation(Operation.OP_SET, i, oldItem, value);
                }
            }
        }

        public Enumerator GetEnumerator() => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        // default Enumerator allocates. we need a custom struct Enumerator to
        // not allocate on the heap.
        // (System.Collections.Generic.List<T> source code does the same)
        //
        // benchmark:
        //   uMMORPG with 800 monsters, Skills.GetHealthBonus() which runs a
        //   foreach on skills SyncList:
        //      before: 81.2KB GC per frame
        //      after:     0KB GC per frame
        // => this is extremely important for MMO scale networking
        public struct Enumerator : IEnumerator<T>
        {
            readonly SyncList<T> list;
            int index;
            public T Current { get; private set; }

            public Enumerator(SyncList<T> list)
            {
                this.list = list;
                index = -1;
                Current = default;
            }

            public bool MoveNext()
            {
                if (++index >= list.Count)
                {
                    return false;
                }
                Current = list[index];
                return true;
            }

            public void Reset() => index = -1;
            object IEnumerator.Current => Current;
            public void Dispose() {}
        }
    }
}
