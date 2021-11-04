/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.FieldMappings.cs
   
   @project: Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant
{
    using System.Series;
    using System.Uniques;

    /// <summary>
    /// Defines the <see cref="FieldMapping" />.
    /// </summary>
    [Serializable]
    public class FieldMapping
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldMapping"/> class.
        /// </summary>
        /// <param name="dbDeckName">The dbDeckName<see cref="string"/>.</param>
        public FieldMapping(string dbDeckName) : this(dbDeckName, new Deck<int>(), new Deck<int>())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldMapping"/> class.
        /// </summary>
        /// <param name="dbDeckName">The dbDeckName<see cref="string"/>.</param>
        /// <param name="keyOrdinal">The keyOrdinal<see cref="IDeck{int}"/>.</param>
        /// <param name="columnOrdinal">The columnOrdinal<see cref="IDeck{int}"/>.</param>
        public FieldMapping(string dbDeckName, IDeck<int> keyOrdinal, IDeck<int> columnOrdinal)
        {
            KeyOrdinal = keyOrdinal;
            ColumnOrdinal = columnOrdinal;
            DbTableName = dbDeckName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ColumnOrdinal.
        /// </summary>
        public IDeck<int> ColumnOrdinal { get; set; }

        /// <summary>
        /// Gets or sets the DbTableName.
        /// </summary>
        public string DbTableName { get; set; }

        /// <summary>
        /// Gets or sets the KeyOrdinal.
        /// </summary>
        public IDeck<int> KeyOrdinal { get; set; }

        #endregion
    }

    /// <summary>
    /// Defines the <see cref="FieldMappings" />.
    /// </summary>
    [Serializable]
    public class FieldMappings : AlbumBase<FieldMapping>
    {
        #region Methods

        /// <summary>
        /// The EmptyDeck.
        /// </summary>
        /// <param name="size">The size<see cref="int"/>.</param>
        /// <returns>The <see cref="ICard{FieldMapping}[]"/>.</returns>
        public override ICard<FieldMapping>[] EmptyDeck(int size)
        {
            return new Card<FieldMapping>[size];
        }

        /// <summary>
        /// The EmptyCard.
        /// </summary>
        /// <returns>The <see cref="ICard{FieldMapping}"/>.</returns>
        public override ICard<FieldMapping> EmptyCard()
        {
            return new Card<FieldMapping>();
        }

        /// <summary>
        /// The EmptyCardTable.
        /// </summary>
        /// <param name="size">The size<see cref="int"/>.</param>
        /// <returns>The <see cref="ICard{FieldMapping}[]"/>.</returns>
        public override ICard<FieldMapping>[] EmptyCardTable(int size)
        {
            return new Card<FieldMapping>[size];
        }

        /// <summary>
        /// The NewCard.
        /// </summary>
        /// <param name="value">The value<see cref="FieldMapping"/>.</param>
        /// <returns>The <see cref="ICard{FieldMapping}"/>.</returns>
        public override ICard<FieldMapping> NewCard(FieldMapping value)
        {
            return new Card<FieldMapping>(value.DbTableName.UniqueKey(), value);
        }

        /// <summary>
        /// The NewCard.
        /// </summary>
        /// <param name="value">The value<see cref="ICard{FieldMapping}"/>.</param>
        /// <returns>The <see cref="ICard{FieldMapping}"/>.</returns>
        public override ICard<FieldMapping> NewCard(ICard<FieldMapping> value)
        {
            return new Card<FieldMapping>(value);
        }

        /// <summary>
        /// The NewCard.
        /// </summary>
        /// <param name="key">The key<see cref="object"/>.</param>
        /// <param name="value">The value<see cref="FieldMapping"/>.</param>
        /// <returns>The <see cref="ICard{FieldMapping}"/>.</returns>
        public override ICard<FieldMapping> NewCard(object key, FieldMapping value)
        {
            return new Card<FieldMapping>(key, value);
        }

        /// <summary>
        /// The NewCard.
        /// </summary>
        /// <param name="key">The key<see cref="ulong"/>.</param>
        /// <param name="value">The value<see cref="FieldMapping"/>.</param>
        /// <returns>The <see cref="ICard{FieldMapping}"/>.</returns>
        public override ICard<FieldMapping> NewCard(ulong key, FieldMapping value)
        {
            return new Card<FieldMapping>(key, value);
        }

        #endregion
    }
}
