using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PhotoGallery.DataTypes;

public class ObservableLinkedList<T> : INotifyCollectionChanged, IEnumerable
{
    private LinkedList<T> _baseList;

    public int Count
    {
        get { return _baseList.Count; }
    }

    public LinkedListNode<T> First
    {
        get { return _baseList.First; }
    }

    public LinkedListNode<T> Last
    {
        get { return _baseList.Last; }
    }

    public ObservableLinkedList()
    {
        _baseList = new LinkedList<T>();
    }

    public ObservableLinkedList(IEnumerable<T> collection)
    {
        _baseList = new LinkedList<T>(collection);
    }

    public LinkedListNode<T> AddAfter(LinkedListNode<T> prevNode, T value)
    {
        LinkedListNode<T> ret = _baseList.AddAfter(prevNode, value);
        OnNotifyCollectionChanged();
        return ret;
    }

    public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
        _baseList.AddAfter(node, newNode);
        OnNotifyCollectionChanged();
    }

    public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
    {
        LinkedListNode<T> ret = _baseList.AddBefore(node, value);
        OnNotifyCollectionChanged();
        return ret;
    }

    public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
    {
        _baseList.AddBefore(node, newNode);
        OnNotifyCollectionChanged();
    }

    public LinkedListNode<T> AddFirst(T value)
    {
        LinkedListNode<T> ret = _baseList.AddFirst(value);
        OnNotifyCollectionChanged();
        return ret;
    }

    public void AddFirst(LinkedListNode<T> node)
    {
        _baseList.AddFirst(node);
        OnNotifyCollectionChanged();
    }

    public LinkedListNode<T> AddLast(T value)
    {
        LinkedListNode<T> ret = _baseList.AddLast(value);
        OnNotifyCollectionChanged();
        return ret;
    }

    public void AddLast(LinkedListNode<T> node)
    {
        _baseList.AddLast(node);
        OnNotifyCollectionChanged();
    }

    public void Clear()
    {
        _baseList.Clear();
        OnNotifyCollectionChanged();
    }

    public bool Contains(T value)
    {
        return _baseList.Contains(value);
    }

    public void CopyTo(T[] array, int index)
    {
        _baseList.CopyTo(array, index);
    }

    public bool LinkedListEquals(object obj)
    {
        return _baseList.Equals(obj);
    }

    public LinkedListNode<T> Find(T value)
    {
        return _baseList.Find(value);
    }

    public LinkedListNode<T> FindLast(T value)
    {
        return _baseList.FindLast(value);
    }

    public Type GetLinkedListType()
    {
        return _baseList.GetType();
    }

    public bool Remove(T value)
    {
        bool ret = _baseList.Remove(value);
        OnNotifyCollectionChanged();
        return ret;
    }

    public void Remove(LinkedListNode<T> node)
    {
        _baseList.Remove(node);
        OnNotifyCollectionChanged();
    }

    public void RemoveFirst()
    {
        _baseList.RemoveFirst();
        OnNotifyCollectionChanged();
    }

    public void RemoveLast()
    {
        _baseList.RemoveLast();
        OnNotifyCollectionChanged();
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;
    public void OnNotifyCollectionChanged()
    {
        if (CollectionChanged != null)
        {
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (_baseList as IEnumerable).GetEnumerator();
    }
}