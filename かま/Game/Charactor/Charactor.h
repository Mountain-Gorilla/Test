#pragma once

// Stage�I�u�W�F�N�g�L����

class Chara : public Object
{
private:

	//3D���b�V�����f��
	SPtr<KdGameModel> m_model;

	// ���f���t�@�C����
	std::string m_modelFileName;

	// �{�[������p
	KdBoneController  m_Bone;

	// �A�j���[�^
	KdAnimator        m_Anime;

	// HP
	int               n_Hp = 100;
public:

	// �X�V����
	virtual void Update() override;

	// �`�揈��         ��override������͕̂K�����邱��
	virtual void Draw() override;

	virtual void ImGuiUpdate() override;

	// �f�V���A���C�Y
	virtual void Deserialize(const json11::Json& jsonObj) override;

	virtual void Serialize(json11::Json::object& outJson)override;

	void SetModel(const std::string& _modelfilename );

	void SetHp(int _hp) { n_Hp = _hp; }

};