import { pool } from "../config/db.mjs";

// GET /api/photos
export const getAllPhotos = async (req, res) => {
  try {
    const [rows] = await pool.query(
      "SELECT * FROM t_photos WHERE isVisible = TRUE"
    );
    res.json(rows);
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: "Failed to fetch photos" });
  }
};

// GET /api/photos/:id
export const getPhotoById = async (req, res) => {
  try {
    const [rows] = await pool.query(
      "SELECT * FROM t_photos WHERE photoId = ?",
      [req.params.id]
    );

    if (rows.length === 0)
      return res.status(404).json({ error: "Photo not found" });

    res.json(rows[0]);
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: "Failed to fetch photo" });
  }
};

// POST /api/photos
export const createPhoto = async (req, res) => {
  const { photoTitle, photoUrl, uploadDate, status, tags } = req.body;
  const idUserLogin = req.user.id;

  const connection = await pool.getConnection();

  try {
    await connection.beginTransaction();

    const [photographerRows] = await connection.query(
      "SELECT photographerId FROM t_photographers WHERE fkUser = ?", 
      [idUserLogin]
    );

    if (photographerRows.length === 0) {
      await connection.rollback();
      return res.status(403).json({ error: "Utilisateur pas valide" });
    }

    const photographerId = photographerRows[0].photographerId;

    const sqlPhoto = `
      INSERT INTO t_photos (photoTitle, photoUrl, uploadDate, isVisible, status, fkPhotographer)
      VALUES (?, ?, ?, TRUE, ?, ?)
    `;

    const [photoResult] = await connection.query(sqlPhoto, [
      photoTitle,
      photoUrl,
      uploadDate || new Date(),
      status || 'available',
      photographerId
    ]);

    const newPhotoId = photoResult.insertId;

    if (tags && Array.isArray(tags) && tags.length > 0) {
      const tagQueries = tags.map(tagId => [newPhotoId, tagId]);
      await connection.query(
        "INSERT INTO t_photo_tags (fkPhoto, fkTag) VALUES ?", 
        [tagQueries]
      );
    }

    await connection.commit();
    res.json({ message: "Photo et tag", photoId: newPhotoId });

  } catch (err) {
    // Si algo falla, deshacemos todo lo anterior
    await connection.rollback();
    console.error(err);
    res.status(500).json({ error: "Error" });
  } finally {
    connection.release();
  }
};

// PUT /api/photos/:id
export const updatePhoto = async (req, res) => {
  const { photoTitle, status, isVisible } = req.body;

  try {
    const sql = `
      UPDATE t_photos
      SET photoTitle = ?, status = ?, isVisible = ?
      WHERE photoId = ?
    `;

    await pool.query(sql, [
      photoTitle,
      status,
      isVisible,
      req.params.id
    ]);

    res.json({ message: "Photo updated successfully" });
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: "Failed to update photo" });
  }
};

// DELETE /api/photos/:id
export const deletePhoto = async (req, res) => {
  try {
    await pool.query("DELETE FROM t_photos WHERE photoId = ?", [
      req.params.id
    ]);

    res.json({ message: "Photo deleted successfully" });
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: "Failed to delete photo" });
  }
};

// GET /api/photos/popular
export const getPopularPhotos = async (req, res) => {
  try {
    const sql = `
      SELECT p.*, COUNT(c.contractId) as totalSales
      FROM t_photos p
      LEFT JOIN t_contracts c ON p.photoId = c.fkPhoto
      WHERE p.isVisible = TRUE
      GROUP BY p.photoId
      ORDER BY totalSales DESC, p.uploadDate DESC
    `;
    const [rows] = await pool.query(sql);
    res.json(rows);
  } catch (err) {
    res.status(500).json({ error: "Error" });
  }
};
// GET /api/photos/tag
export const getPhotosByTag = async (req, res) => {
  const { tagName } = req.params;
  try {
    const sql = `
      SELECT p.* FROM t_photos p
      JOIN t_photo_tags pt ON p.photoId = pt.fkPhoto
      JOIN t_tags t ON pt.fkTag = t.tagId
      WHERE t.tagName = ? AND p.isVisible = TRUE
    `;
    const [rows] = await pool.query(sql, [tagName]);
    res.json(rows);
  } catch (err) {
    res.status(500).json({ error: "Error" });
  }
};

