namespace Factory.DAL.Enums.Stores
{
    public enum WarehouseType
    {
        RawGlassStorage,          // For storing raw glass sheets or panels received from suppliers
        CutGlassStorage,          // For storing glass after it has been cut to size
        TemperedGlassStorage,     // For storing tempered glass (heat-treated for strength)
        LaminatedGlassStorage,    // For storing laminated glass (sandwiched layers for safety)
        InsulatedGlassStorage,    // For storing insulated glass units (IGUs) used in windows
        CoatedGlassStorage,       // For storing glass with special coatings (e.g., low-E, reflective)
        MirroredGlassStorage,     // For storing mirrored glass products
        PatternedGlassStorage,    // For storing patterned or textured glass
        SafetyGlassStorage,       // For storing safety glass (e.g., bulletproof, fire-resistant)
        FinishedProductStorage,   // For storing fully processed and ready-to-ship glass products
        ScrapGlassStorage,        // For storing defective or leftover glass for recycling
        ExportWarehouse,          // For glass products designated for export
        ImportWarehouse,          // For raw glass or materials imported for processing
        PackagingArea,            // For storing packaging materials to protect glass during shipping
        QualityControlStorage     // For glass products undergoing inspection or quality checks
    }
}
