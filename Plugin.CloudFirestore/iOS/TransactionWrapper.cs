﻿using System;
using Firebase.CloudFirestore;
using Foundation;
using System.Linq;

namespace Plugin.CloudFirestore
{
    public class TransactionWrapper : ITransaction, IEquatable<TransactionWrapper>
    {
        private readonly Transaction _transaction;

        public TransactionWrapper(Transaction transaction)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public IDocumentSnapshot GetDocument(IDocumentReference document)
        {
            return Get(document);
        }

        public IDocumentSnapshot Get(IDocumentReference document)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            var snapshot = _transaction.GetDocument((DocumentReference)wrapper, out var error);

            if (error != null)
            {
                throw ExceptionMapper.Map(error);
            }

            return new DocumentSnapshotWrapper(snapshot);
        }

        public void SetData(IDocumentReference document, object documentData)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper);
        }

        public ITransaction Set<T>(IDocumentReference document, T documentData)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper);
            return this;
        }

        public void SetData(IDocumentReference document, object documentData, params string[] mergeFields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, mergeFields);
        }

        public ITransaction Set<T>(IDocumentReference document, T documentData, params string[] mergeFields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, mergeFields);
            return this;
        }

        public void SetData(IDocumentReference document, object documentData, params FieldPath[] mergeFields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, mergeFields.Select(x => x.ToNative()).ToArray());
        }

        public ITransaction Set<T>(IDocumentReference document, T documentData, params FieldPath[] mergeFields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, mergeFields.Select(x => x.ToNative()).ToArray());
            return this;
        }

        public void SetData(IDocumentReference document, object documentData, bool merge)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, merge);
        }

        public ITransaction Set<T>(IDocumentReference document, T documentData, bool merge)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.SetData(documentData.ToNativeFieldValues(), (DocumentReference)wrapper, merge);
            return this;
        }

        public void UpdateData(IDocumentReference document, object fields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.UpdateData(fields.ToNativeFieldValues(), (DocumentReference)wrapper);
        }

        public ITransaction Update<T>(IDocumentReference document, T fields)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.UpdateData(fields.ToNativeFieldValues(), (DocumentReference)wrapper);
            return this;
        }

        public void UpdateData(IDocumentReference document, string field, object? value, params object?[] moreFieldsAndValues)
        {
            Update(document, field, value, moreFieldsAndValues);
        }

        public ITransaction Update(IDocumentReference document, string field, object? value, params object?[] moreFieldsAndValues)
        {
            var fields = Field.CreateFields(field, value, moreFieldsAndValues);
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.UpdateData(fields, (DocumentReference)wrapper);
            return this;
        }

        public void UpdateData(IDocumentReference document, FieldPath field, object? value, params object?[] moreFieldsAndValues)
        {
            Update(document, field, value, moreFieldsAndValues);
        }

        public ITransaction Update(IDocumentReference document, FieldPath field, object? value, params object?[] moreFieldsAndValues)
        {
            var fields = Field.CreateFields(field, value, moreFieldsAndValues);
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.UpdateData(fields, (DocumentReference)wrapper);
            return this;
        }

        public void DeleteDocument(IDocumentReference document)
        {
            Delete(document);
        }

        public ITransaction Delete(IDocumentReference document)
        {
            var wrapper = (DocumentReferenceWrapper)document;
            _transaction.DeleteDocument((DocumentReference)wrapper);
            return this;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as TransactionWrapper);
        }

        public bool Equals(TransactionWrapper? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            if (ReferenceEquals(_transaction, other._transaction)) return true;
            return _transaction.Equals(other._transaction);
        }

        public override int GetHashCode()
        {
            return _transaction?.GetHashCode() ?? 0;
        }
    }
}
