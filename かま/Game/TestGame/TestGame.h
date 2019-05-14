#pragma once

// Stage�I�u�W�F�N�g�L����

class StageObject: public Object
{
private:
	//3D���b�V�����f��
	SPtr<KdGameModel> m_model;
	
	// ���f���t�@�C����
	std::string m_modelFileName;

public:

	// �����ݒ�֐�
	void SetModel(const std::string& _modelfilename);

	// �`�揈��         ��override������͕̂K�����邱��
	virtual void Draw() override;

	// ImGui��`��
	virtual void ImGuiUpdate() override;

	// �f�V���A���C�Y
	virtual void Deserialize(const json11::Json& jsonObj) override;

	virtual void Serialize(json11::Json::object& outJson)override;
};