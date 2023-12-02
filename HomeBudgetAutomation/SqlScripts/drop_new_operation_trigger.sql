DROP TRIGGER IF EXISTS check_update_operation ON operations;
DROP FUNCTION IF EXISTS prevent_update_operation();
DROP TRIGGER IF EXISTS check_insert_operation ON operations;
DROP FUNCTION IF EXISTS prevent_insert_operation();