using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using LiteDB;
using HMDDistanceReader.Databases.Interfaces;

namespace HMDDistanceReader.Databases
{
    public class LiteDbReader : IDisposable, ILiteDbReader
    {
        private bool disposedValue;
        private readonly object _lockObj = new object();
        private LiteDatabase _db;
        private string _errorMessage;
        private string _dbPath;
        private bool _isConnected;

        public string ErrorMessage => this._errorMessage;
        public bool IsConnected => this._isConnected;

        static LiteDbReader()
        {
            BsonMapper.Global.EnumAsInteger = true;
        }

        public LiteDbReader(string dbPath)
        {
            this._dbPath = dbPath;
            this.Connect();
        }

        private void Connect()
        {
            lock (this._lockObj)
            {
                this._isConnected = false;
                if (this._db != null)
                    this._db.Dispose();
                if (!File.Exists(_dbPath))
                {
                    this._errorMessage = $"Database file not found: {_dbPath}";
                    return;
                }
                try
                {
                    this._db = new LiteDatabase($"Filename=\"{_dbPath}\";Connection=Direct;ReadOnly=true");
                }
                catch (Exception ex)
                {
                    this._errorMessage = $"Database open error: {ex.Message}";
                    return;
                }
                this._isConnected = true;
            }
        }
        private void Disconnect()
        {
            lock (this._lockObj)
            {
                this._isConnected = false;
                if (this._db == null)
                    return;
                this._db.Dispose();
                this._db = null;
            }
        }
        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> predicate, int skip = 0, int limit = int.MaxValue)
        {
            return this._db.GetCollection<T>(typeof(T).Name).Find(predicate, skip, limit);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    this.Disconnect();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                this.disposedValue = true;
            }
        }
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
