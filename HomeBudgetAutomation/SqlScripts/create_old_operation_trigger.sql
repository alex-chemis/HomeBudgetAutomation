CREATE OR REPLACE FUNCTION prevent_delete_operation()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD.balance_id IS NOT NULL THEN
        RAISE EXCEPTION 'Cannot delete a operation recorded in the balance sheet';
    END IF;

    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS check_delete_operation ON operations;

CREATE TRIGGER check_delete_operation
BEFORE DELETE ON operations
FOR EACH ROW
EXECUTE FUNCTION prevent_delete_operation();
